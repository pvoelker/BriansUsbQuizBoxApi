using BriansUsbQuizBoxApi.Exceptions;
using BriansUsbQuizBoxApi.Protocols;
using BriansUsbQuizBoxApi.StateMachines;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace BriansUsbQuizBoxApi
{
    /// <summary>
    /// Event based implementation for Brian's Quiz Box
    /// </summary>
    public class QuizBoxApi : IQuizBoxApi
    {
        private bool _disposedValue;

        static readonly private BoxCommandReport _statusRequest = new BoxCommandReport { CommandHeader = CommandHeaderByte.STATUS_REQUEST };

        private bool _idleMode = false;

        private WinnerByteSM _winnerByteSM;
        private StatusByteSM _statusByteSM;
        private GameStatusByteSM _gameStatusByteSM;

        private EventWaitHandle? _done = null;
        private EventWaitHandle? _doneComplete = null;

        private ConcurrentQueue<BoxCommandReport> _commands = new ConcurrentQueue<BoxCommandReport>();

        private IQuizBoxCoreApi _api;

        private int? _threadId;

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
        public event EventHandler<GameDoneEventArgs>? GameDone;
        
        /// <inheritdoc/>
        public event EventHandler<BuzzInStatsEventArgs>? BuzzInStats;
        
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
                (paddle) => BuzzIn?.Invoke(this, new BuzzInEventArgs(paddle)),
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
                (p, r1, r2, r3, r4, g1, g2, g3, g4) => GameDone?.Invoke(this, new GameDoneEventArgs(p, r1, r2, r3, r4, g1, g2, g3, g4)),
                (p, r1, r2, r3, r4, g1, g2, g3, g4) => BuzzInStats?.Invoke(this, new BuzzInStatsEventArgs(p, r1, r2, r3, r4, g1, g2, g3, g4))
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
                        var thread = new Thread(new ThreadStart(ReadData));
                        thread.Name = "Quiz Box I/O Thread";
                        thread.Start();

                        _threadId = thread.ManagedThreadId;
                    }
                    catch
                    {
                        _threadId = null;

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
        public void Disconnect()
        {
            if (_api != null)
            {
                if (_done != null)
                {
                    _done.Set();
                    _doneComplete?.WaitOne();
                }

                if (_done != null)
                {
                    _done.Dispose();
                    _done = null;
                }

                if (_doneComplete != null)
                {
                    _doneComplete.Dispose();
                    _doneComplete = null;
                }

                _threadId = null;
            }
        }

        /// <inheritdoc/>
        /// <exception cref="NotConnectedException">Quiz box is not yet connected</exception>
        /// <exception cref="InvalidOperationException">Call was made from Quiz Box I/O thread</exception>
        public void Reset()
        {
            if (_api != null)
            {
                CheckCommandThreadAndThrow();

                _commands.Enqueue(new BoxCommandReport { CommandHeader = CommandHeaderByte.CLEAR });
            }
            else
            {
                throw new NotConnectedException("Must connect before commands");
            }
        }

        /// <inheritdoc/>
        /// <exception cref="NotConnectedException">Quiz box is not yet connected</exception>
        /// <exception cref="InvalidOperationException">Call was made from Quiz Box I/O thread</exception>
        public void Start5SecondBuzzInTimer()
        {
            if (_api != null)
            {
                CheckCommandThreadAndThrow();

                _commands.Enqueue(new BoxCommandReport { CommandHeader = CommandHeaderByte.START_5_SEC_TIMER });
            }
            else
            {
                throw new NotConnectedException("Must connect before commands");
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentOutOfRangeException">An invalid timer length has been given</exception>
        /// <exception cref="NotConnectedException">Quiz box is not yet connected</exception>
        /// <exception cref="InvalidOperationException">Call was made from Quiz Box I/O thread</exception>
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
                CheckCommandThreadAndThrow();

                _commands.Enqueue(new BoxCommandReport { CommandHeader = command });
            }
            else
            {
                throw new NotConnectedException("Must connect before commands");
            }
        }

        /// <inheritdoc/>
        /// <exception cref="NotConnectedException">Quiz box is not yet connected</exception>
        /// <exception cref="InvalidOperationException">Call was made from Quiz Box I/O thread</exception>
        public void StartPaddleLockout()
        {
            if (_api != null)
            {
                CheckCommandThreadAndThrow();

                _commands.Enqueue(new BoxCommandReport { CommandHeader = CommandHeaderByte.START_INFINITE_TIMER });
            }
            else
            {
                throw new NotConnectedException("Must connect before commands");
            }
        }

        /// <inheritdoc/>
        /// <exception cref="NotConnectedException">Quiz box is not yet connected</exception>
        /// <exception cref="InvalidOperationException">Call was made from Quiz Box I/O thread</exception>
        public void StopPaddleLockout()
        {
            if (_api != null)
            {
                CheckCommandThreadAndThrow();

                _commands.Enqueue(new BoxCommandReport { CommandHeader = CommandHeaderByte.END_INFINITE_TIMER_BUZZ });
            }
            else
            {
                throw new NotConnectedException("Must connect before commands");
            }
        }

        /// <inheritdoc/>
        /// <exception cref="NotConnectedException"></exception>
        /// <exception cref="InvalidOperationException">Call was made from Quiz Box I/O thread</exception>
        public void StartReactionTimingGame()
        {
            if (_api != null)
            {
                CheckCommandThreadAndThrow();

                _commands.Enqueue(new BoxCommandReport { CommandHeader = CommandHeaderByte.START_REACTION_TIMING_GAME });
            }
            else
            {
                throw new NotConnectedException("Must connect before commands");
            }
        }

        private void CheckCommandThreadAndThrow()
        {
            if(_threadId != null)
            {
                if(Thread.CurrentThread.ManagedThreadId == _threadId)
                {
                    throw new InvalidOperationException("Cannot execute commands from the Quiz Box IO Thread");
                }
            }
        }

        private void ReadData()
        {
            // Force a reset of the quiz box
            _api.WriteCommand(new BoxCommandReport { CommandHeader = CommandHeaderByte.CLEAR });

            while (_done != null && _done.WaitOne(10) == false)
            {
                try
                {
                    WriteData();
                }
                catch (DisconnectionException ex)
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

                BoxStatusReport? status = null;
                try
                {
                    status = _api.ReadStatus();
                }
                catch (TimeoutException)
                {
                    // Keep moving on
                }
                catch (DisconnectionException ex)
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
            }

            if (_doneComplete != null)
            {
                _doneComplete.Set();
            }

            _api.Disconnect();
        }

        private void WriteData()
        {
            if (_commands.TryPeek(out var command))
            {
                _api.WriteCommand(command);

                if(IsStatusReturned(command.CommandHeader) == false)
                {
                    Thread.Sleep(10);

                    // Request status if needed
                    _api.WriteCommand(_statusRequest);
                }
            }
            else
            {
                _api.WriteCommand(_statusRequest);
            }
        }

        private void ProcessRead(BoxStatusReport status)
        {
            _winnerByteSM.Process(status.Status, status.Winner);

            _statusByteSM.Process(status.Status);

            _gameStatusByteSM.Process(status);

            if (_commands.TryPeek(out var command))
            {
                var expectedFunc = GetExpectedStatusLogic(command.CommandHeader);

                if(expectedFunc.Invoke(status.Status) == true)
                {
                    // We see the state we are expecting, pull the command off the queue
                    _commands.TryDequeue(out var _);
                }
                else
                {
                    // Keep retrying the command until we get the expected state
                }
            }

            if (status.Status == StatusByte.IDLE_MODE)
            {
                if (_idleMode == false)
                {
                    _winnerByteSM.Reset();
                    _statusByteSM.Reset();
                    _gameStatusByteSM.Reset();

                    _idleMode = true;
                }
            }
            else
            {
                _idleMode = false;
            }
        }

        protected bool IsStatusReturned(CommandHeaderByte command)
        {
            if (command == CommandHeaderByte.START_REACTION_TIMING_GAME)
            {
                return true;
            }
            else if (command == CommandHeaderByte.START_5_SEC_TIMER)
            {
                return true;
            }
            else if (command == CommandHeaderByte.STATUS_REQUEST)
            {
                return true;
            }

            return false;
        }

        protected Func<StatusByte, bool> GetExpectedStatusLogic(CommandHeaderByte command)
        {
            if(command == CommandHeaderByte.CLEAR)
            {
                return (x) => x == StatusByte.IDLE_MODE;
            }
            else if(command == CommandHeaderByte.START_REACTION_TIMING_GAME)
            {
                return (x) => x == StatusByte.GAME_PRESTART;
            }
            else if(command == CommandHeaderByte.START_5_SEC_TIMER)
            {
                return (x) => x == StatusByte.RUNNING_5_SEC_TIMER;
            }
            else if(command == CommandHeaderByte.START_30_SEC_TIMER)
            {
                return (x) => x == StatusByte.EXTENDED_TIMER_RUNNING;
            }
            else if (command == CommandHeaderByte.START_1_MIN_TIMER)
            {
                return (x) => x == StatusByte.EXTENDED_TIMER_RUNNING;
            }
            else if (command == CommandHeaderByte.START_2_MIN_TIMER)
            {
                return (x) => x == StatusByte.EXTENDED_TIMER_RUNNING;
            }
            else if (command == CommandHeaderByte.START_3_MIN_TIMER)
            {
                return (x) => x == StatusByte.EXTENDED_TIMER_RUNNING;
            }
            else if (command == CommandHeaderByte.START_INFINITE_TIMER)
            {
                return (x) => x == StatusByte.EXTENDED_TIMER_RUNNING;
            }
            else if (command == CommandHeaderByte.END_INFINITE_TIMER_BUZZ)
            {
                // There is no way to track the state change for this
                // After this command is sent, the state EXTENDED_TIMER_RUNNING sticks around for a bit
                // Resending the command sends the API into an infinite paddle lockout loop
                return (x) => true;
            }
            else if (command == CommandHeaderByte.STATUS_REQUEST)
            {
                return (x) => true; // Don't need to monitor state changes for status requests
            }
            else
            {
                throw new InvalidOperationException($"Command header byte '{command}' not handled");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Disconnect();

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
