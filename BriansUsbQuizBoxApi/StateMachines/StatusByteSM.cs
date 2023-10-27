using BriansUsbQuizBoxApi.Protocol;
using System;

namespace BriansUsbQuizBoxApi.StateMachines
{
    public delegate void GameWaitingCallback();

    public delegate void GameLightOnCallback();

    public delegate void GameBuzzInCallback();

    public delegate void GameDoneCallback();

    /// <summary>
    /// Status Byte state machine for quiz box game mode
    /// </summary>
    internal class GameStatusByteSM
    {
        private enum GameStates { Off, Waiting, LightOn, BuzzIn, Done }

        private GameStates _lastGameState = GameStates.Off;

        private GameWaitingCallback _gameWaitingCallback;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="quizBoxReadyCallback">Callback for quiz box ready event</param>
        /// <param name="fiveSecondTimerStartedCallback">Callback for five second timer started event</param>
        /// <param name="lockoutTimerStartedCallback">Callback for lockout timer started event</param>
        /// <param name="lockoutTimerExpiredCallback">Callback for lockout timer stopped event</param>
        /// <exception cref="ArgumentNullException">One or more arguments passed in where null</exception>
        public GameStatusByteSM(
            QuizBoxReadyCallback quizBoxReadyCallback,
            FiveSecondTimerStartedCallback fiveSecondTimerStartedCallback,
            LockoutTimerStartedCallback lockoutTimerStartedCallback,
            LockoutTimerExpiredCallback lockoutTimerExpiredCallback)
        {
            if (quizBoxReadyCallback == null)
            {
                throw new ArgumentNullException(nameof(quizBoxReadyCallback));
            }
            if (fiveSecondTimerStartedCallback == null)
            {
                throw new ArgumentNullException(nameof(fiveSecondTimerStartedCallback));
            }
            if (lockoutTimerStartedCallback == null)
            {
                throw new ArgumentNullException(nameof(lockoutTimerStartedCallback));
            }
            if (lockoutTimerExpiredCallback == null)
            {
                throw new ArgumentNullException(nameof(lockoutTimerExpiredCallback));
            }

            _quizBoxReadyCallback = quizBoxReadyCallback;
            _fiveSecondTimerStartedCallback = fiveSecondTimerStartedCallback;
            _lockoutTimerStartedCallback = lockoutTimerStartedCallback;
            _lockoutTimerExpiredCallback = lockoutTimerExpiredCallback;
        }

        /// <summary>
        /// Reset state machine to reset state of quiz box
        /// </summary>
        public void Reset()
        {
            _lastGameState = GameStates.Off
        }

        /// <summary>
        /// Process a new winner byte
        /// </summary>
        /// <param name="statusByte">Status byte</param>
        public void Process(StatusByte statusByte)
        {
            var idleMode = statusByte == StatusByte.IDLE_MODE;
            var fiveSecondTimerStarted = statusByte == StatusByte.RUNNING_5_SEC_TIMER;
            var lockoutTimerStarted = statusByte == StatusByte.EXTENDED_TIMER_RUNNING;

            if(idleMode != _lastInitialIdleMode)
            {
                if(idleMode == true)
                {
                    _quizBoxReadyCallback();
                }
                _lastInitialIdleMode = true;
            }

            if (fiveSecondTimerStarted != _lastFiveSecondTimerStarted)
            {
                if (fiveSecondTimerStarted == true)
                {
                    _fiveSecondTimerStartedCallback();
                }
                _lastFiveSecondTimerStarted = fiveSecondTimerStarted;
            }

            if (lockoutTimerStarted != _lastLockoutTimerStarted)
            {
                if (lockoutTimerStarted == true)
                {
                    _lockoutTimerStartedCallback();
                }
                else
                {
                    _lockoutTimerExpiredCallback();
                }
                _lastLockoutTimerStarted = lockoutTimerStarted;
            }
        }
    }
}
