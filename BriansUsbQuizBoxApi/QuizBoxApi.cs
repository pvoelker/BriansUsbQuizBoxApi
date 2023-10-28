using BriansUsbQuizBoxApi.Exceptions;
using BriansUsbQuizBoxApi.Protocols;
using BriansUsbQuizBoxApi.StateMachines;
using System;
using System.Diagnostics;
using System.Threading;

namespace BriansUsbQuizBoxApi
{
    public class QuizBoxApi : IDisposable
    {
        private bool _disposedValue;

        private WinnerByteSM _winnerByteSM;
        private StatusByteSM _statusByteSM;
        private GameStatusByteSM _gameStatusByteSM;

        private EventWaitHandle? _done = null;
        private EventWaitHandle? _doneComplete = null;

        private QuizBoxCoreApi _api = new QuizBoxCoreApi();

        #region Events

        /// <summary>
        /// Event fired when quiz box is ready
        /// </summary>
        public event EventHandler? QuizBoxReady;

        /// <summary>
        /// Event fired when someone buzzes in
        /// </summary>
        public event EventHandler<BuzzInEventArgs>? BuzzIn;

        /// <summary>
        /// Event fired when the five second timer is started
        /// </summary>
        /// <remarks>This event is not meant for exact timings</remarks>
        public event EventHandler? FiveSecondTimerStarted;

        /// <summary>
        /// Event fired when the five second timer is expired. Paddles are locked out
        /// </summary>
        /// <remarks>This event is not meant for exact timings</remarks>
        public event EventHandler? FiveSecondTimerExpired;

        /// <summary>
        /// Event fired when a paddle lockout timer is started
        /// </summary>
        /// <remarks>This event is not meant for exact timings</remarks>
        public event EventHandler? LockoutTimerStarted;

        /// <summary>
        /// Event fired when a paddle lockout timer is expired
        /// </summary>
        /// <remarks>This event is not meant for exact timings</remarks>
        public event EventHandler? LockoutTimerExpired;

        /// <summary>
        /// Event fired when a reaction time game is started. Players need to watch for the yellow light to come on the quiz box
        /// </summary>
        /// <remarks>This event is not meant for exact timings</remarks>
        public event EventHandler? GameStarted;

        /// <summary>
        /// Event fired when the yellow quiz box light comes on.  Players need to press their paddles as quickly as possible
        /// </summary>
        /// <remarks>This event is not meant for exact timings</remarks>
        public event EventHandler? GameLightOn;

        /// <summary>
        /// Event fired when the first player of the reaction time game buzzes in
        /// </summary>
        /// <remarks>This event is not meant for exact timings</remarks>
        public event EventHandler? GameFirstBuzzIn;

        /// <summary>
        /// Event fire when the reaction time game has completed.  Results are included in the event arguments
        /// </summary>
        /// <remarks>This event is not meant for exact timings</remarks>
        public event EventHandler<GameDoneEventArgs>? GameDone;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public QuizBoxApi()
        {
            _winnerByteSM = new WinnerByteSM(
                (paddleColor, paddleNumber) => BuzzIn?.Invoke(this, new BuzzInEventArgs(paddleColor, paddleNumber)),
                () => FiveSecondTimerExpired?.Invoke(this, null)
            );
            _statusByteSM = new StatusByteSM(
                () => QuizBoxReady?.Invoke(this, null),
                () => FiveSecondTimerStarted?.Invoke(this, null),
                () => LockoutTimerStarted?.Invoke(this, null),
                () => LockoutTimerExpired?.Invoke(this, null)
            );
            _gameStatusByteSM = new GameStatusByteSM(
                () => GameStarted?.Invoke(this, null),
                () => GameLightOn?.Invoke(this, null),
                () => GameFirstBuzzIn?.Invoke(this, null),
                (r1, r2, r3, r4, g1, g2, g3, g4) => GameDone?.Invoke(this, new GameDoneEventArgs(r1, r2, r3, r4, g1, g2, g3, g4))
            );
        }

        /// <summary>
        /// True if connected to a quiz box, otherwise false
        /// </summary>
        public bool IsConnected
        {
            get { return _api.IsConnected; }
        }

        /// <summary>
        /// Attempt to connect to a quiz box
        /// </summary>
        /// <returns>True if connection successful, otherwise false</returns>
        /// <exception cref="MultipleDevicesException">More than one quiz box is detected</exception>
        /// <exception cref="InvalidOperationException">Already connected to a quiz box</exception>
        public bool Connect()
        {
            if (_api.IsConnected == false)
            {
                if (_api.Connect())
                {
                    _done = new EventWaitHandle(false, EventResetMode.ManualReset);
                    _doneComplete = new EventWaitHandle(false, EventResetMode.ManualReset);

                    try
                    {
                        Thread thread = new Thread(new ThreadStart(ReadData));
                        thread.Name = "Quiz Box Read Thread";
                        thread.Start();
                    }
                    catch
                    {
                        _done.Dispose();
                        _done = null;
                        _doneComplete.Dispose();
                        _doneComplete = null;

                        throw;
                    }
                }
                else
                {
                    return false;
                }

                return true;
            }
            else
            {
                throw new InvalidOperationException("Already connected to a quiz box");
            }
        }

        /// <summary>
        /// Send the reset command to the quiz box.  All timers and game modes are cancelled
        /// </summary>
        /// <exception cref="NotConnectedException">Quiz box is not yet connected</exception>
        public void Reset()
        {
            if (_api != null)
            {
                _api.WriteCommand(new BoxCommandReport {  CommandHeader = CommandHeaderByte.CLEAR });

                _winnerByteSM.Reset();
                _statusByteSM.Reset();
                _gameStatusByteSM.Reset();
            }
            else
            {
                throw new NotConnectedException("Must connect before commands");
            }
        }

        /// <summary>
        /// Send the command to start the five second timer to the quiz box.  Paddles will be locked out after the timer expires
        /// </summary>
        /// <exception cref="NotConnectedException">Quiz box is not yet connected</exception>
        public void Start5SecondBuzzInTimer()
        {
            if (_api != null)
            {
                _api.WriteCommand(new BoxCommandReport { CommandHeader = CommandHeaderByte.START_5_SEC_TIMER });
            }
            else
            {
                throw new NotConnectedException("Must connect before commands");
            }
        }

        /// <summary>
        /// Send the command to start the paddle lockout timer
        /// </summary>
        /// <param name="timer">The length of the lockout timer</param>
        /// <exception cref="ArgumentOutOfRangeException">An invalid timer length has been given</exception>
        /// <exception cref="NotConnectedException">Quiz box is not yet connected</exception>
        public void StartPaddleLockoutTimer(LockoutTimerEnum timer)
        {
            CommandHeaderByte command = 0;

            switch (timer)
            {
                case LockoutTimerEnum.Timer30Seconds:
                    command = CommandHeaderByte.START_30_SEC_TIMER;
                    break;
                case LockoutTimerEnum.Timer1Minute:
                    command = CommandHeaderByte.START_1_MIN_TIMER;
                    break;
                case LockoutTimerEnum.Timer2Minutes:
                    command = CommandHeaderByte.START_2_MIN_TIMER;
                    break;
                case LockoutTimerEnum.Timer3Minutes:
                    command = CommandHeaderByte.START_3_MIN_TIMER;
                    break;
            }

            if(command == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(timer));
            }

            if (_api != null)
            {
                _api.WriteCommand(new BoxCommandReport { CommandHeader = command });
            }
            else
            {
                throw new NotConnectedException("Must connect before commands");
            }
        }

        /// <summary>
        /// Lockout quiz box paddles (indefinite paddle lockout timer)
        /// </summary>
        /// <exception cref="NotConnectedException">Quiz box is not yet connected</exception>
        public void StartPaddleLockout()
        {
            if (_api != null)
            {
                _api.WriteCommand(new BoxCommandReport { CommandHeader = CommandHeaderByte.START_INFINITE_TIMER });
            }
            else
            {
                throw new NotConnectedException("Must connect before commands");
            }
        }

        /// <summary>
        /// Stop the lockout on quiz box paddles
        /// </summary>
        /// <exception cref="NotConnectedException">Quiz box is not yet connected</exception>
        public void StopPaddleLockout()
        {
            if (_api != null)
            {
                _api.WriteCommand(new BoxCommandReport { CommandHeader = CommandHeaderByte.END_INFINITE_TIMER_BUZZ });
            }
            else
            {
                throw new NotConnectedException("Must connect before commands");
            }
        }

        /// <summary>
        /// Attempts to start the reaction time game.  The 'GameStarted' event will fire if the game was started
        /// </summary>
        /// <exception cref="NotConnectedException"></exception>
        public void StartReactionTimingGame()
        {
            if (_api != null)
            {
                _api.WriteCommand(new BoxCommandReport { CommandHeader = CommandHeaderByte.START_REACTION_TIMING_GAME });
            }
            else
            {
                throw new NotConnectedException("Must connect before commands");
            }
        }

        private void RequestStatus()
        {
            if (_api != null)
            {
                _api.WriteCommand(new BoxCommandReport { CommandHeader = CommandHeaderByte.STATUS_REQUEST });
            }
            else
            {
                throw new NotConnectedException("Must connect before commands");
            }
        }

        private void ReadData()
        {
            RequestStatus();

            while (_done != null && _done.WaitOne(25) == false)
            {
                var status = _api.ReadStatus();

                if (status != null)
                {
                    ProcessRead(status);
                }

                RequestStatus();
            }

            if (_doneComplete != null)
            {
                _doneComplete.Set();
            }
        }

        private void ProcessRead(BoxStatusReport status)
        {
            _winnerByteSM.Process(status.Winner);

            _statusByteSM.Process(status.Status);

            _gameStatusByteSM.Process(status);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_done != null)
                    {
                        _done.Set();
                        _doneComplete?.WaitOne();
                    }

                    if (_api != null)
                    {
                        _api.Dispose();
                    }
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
