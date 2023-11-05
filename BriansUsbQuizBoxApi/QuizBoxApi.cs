using BriansUsbQuizBoxApi.Exceptions;
using BriansUsbQuizBoxApi.Protocols;
using BriansUsbQuizBoxApi.StateMachines;
using System;
using System.Threading;

namespace BriansUsbQuizBoxApi
{
    public class QuizBoxApi : IQuizBoxApi
    {
        private bool _disposedValue;

        private WinnerByteSM _winnerByteSM;
        private StatusByteSM _statusByteSM;
        private GameStatusByteSM _gameStatusByteSM;

        private EventWaitHandle? _done = null;
        private EventWaitHandle? _doneComplete = null;

        private IQuizBoxCoreApi _api;

        #region Events

        /// <inheritdoc/>
        public event EventHandler? QuizBoxReady;

        /// <inheritdoc/>
        public event EventHandler<BuzzInEventArgs>? BuzzIn;

        /// <inheritdoc/>
        public event EventHandler? FiveSecondTimerStarted;

        /// <inheritdoc/>
        public event EventHandler? FiveSecondTimerExpired;

        /// <inheritdoc/>
        public event EventHandler? LockoutTimerStarted;

        /// <inheritdoc/>
        public event EventHandler? LockoutTimerExpired;

        /// <inheritdoc/>
        public event EventHandler? GameStarted;

        /// <inheritdoc/>
        public event EventHandler? GameLightOn;

        /// <inheritdoc/>
        public event EventHandler? GameFirstBuzzIn;

        /// <inheritdoc/>
        public event EventHandler<GameDoneEventArgs>? GameDone;

        /// <inheritdoc/>
        public event EventHandler<DisconnectionEventArgs>? ReadThreadDisconnection;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public QuizBoxApi(IQuizBoxCoreApi api)
        {
            _api = api;

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

        /// <inheritdoc/>
        public bool IsConnected
        {
            get { return _api.IsConnected; }
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void Disconnect()
        {
            if (_api != null)
            {
                _api.Disconnect();
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
                BoxStatusReport? status = null;
                try
                {
                    status = _api.ReadStatus();
                }
                catch(DisconnectionException ex)
                {
                    if (ReadThreadDisconnection != null)
                    {
                        ReadThreadDisconnection.Invoke(this, new DisconnectionEventArgs(ex));
                    }
                }
                catch
                {
                    throw;
                }                

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
