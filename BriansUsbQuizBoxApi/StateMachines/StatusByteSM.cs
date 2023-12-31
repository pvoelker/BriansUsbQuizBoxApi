﻿using BriansUsbQuizBoxApi.Protocols;
using System;

namespace BriansUsbQuizBoxApi.StateMachines
{
    public delegate void FiveSecondTimerStartedCallback();

    public delegate void LockoutTimerStartedCallback();

    public delegate void LockoutTimerExpiredCallback();

    /// <summary>
    /// Status Byte state machine
    /// </summary>
    public class StatusByteSM
    {
        private readonly FiveSecondTimerStartedCallback _fiveSecondTimerStartedCallback;
        private readonly LockoutTimerStartedCallback _lockoutTimerStartedCallback;
        private readonly LockoutTimerExpiredCallback _lockoutTimerExpiredCallback;

        private bool _lastFiveSecondTimerStarted = false;
        private bool _lastLockoutTimerStarted = false;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fiveSecondTimerStartedCallback">Callback for five second timer started event</param>
        /// <param name="lockoutTimerStartedCallback">Callback for lockout timer started event</param>
        /// <param name="lockoutTimerExpiredCallback">Callback for lockout timer stopped event</param>
        /// <exception cref="ArgumentNullException">One or more arguments passed in where null</exception>
        public StatusByteSM(
            FiveSecondTimerStartedCallback fiveSecondTimerStartedCallback,
            LockoutTimerStartedCallback lockoutTimerStartedCallback,
            LockoutTimerExpiredCallback lockoutTimerExpiredCallback)
        {
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

            _fiveSecondTimerStartedCallback = fiveSecondTimerStartedCallback;
            _lockoutTimerStartedCallback = lockoutTimerStartedCallback;
            _lockoutTimerExpiredCallback = lockoutTimerExpiredCallback;
        }

        /// <summary>
        /// Reset state machine to reset state of quiz box
        /// </summary>
        public void Reset()
        {
            // Don't reset - _lastIdleModeOneShot
            _lastFiveSecondTimerStarted = false;
            _lastLockoutTimerStarted = false;
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