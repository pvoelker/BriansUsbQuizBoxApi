using BriansUsbQuizBoxApi.Helpers;
using BriansUsbQuizBoxApi.Protocols;
using System;

namespace BriansUsbQuizBoxApi.StateMachines
{
    public delegate void BuzzInCallback(Paddle paddle);

    public delegate void FiveSecondTimerExpiredCallback();

    /// <summary>
    /// Winner Byte state machine
    /// </summary>
    public class WinnerByteSM
    {
        private readonly BuzzInCallback _buzzInCallback;
        private readonly FiveSecondTimerExpiredCallback _fiveSecondTimerExpiredCallback;

        private Paddle? _lastPaddle = null;
        private bool _lastFiveSecondTimerExpired = false;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="buzzInCallback">Callback for buzz in event</param>
        /// <param name="fiveSecondTimerExpiredCallback">Callback for five second timer expire event</param>
        /// <exception cref="ArgumentNullException">One or more arguments passed in where null</exception>
        public WinnerByteSM(
            BuzzInCallback buzzInCallback,
            FiveSecondTimerExpiredCallback fiveSecondTimerExpiredCallback)
        {
            if (buzzInCallback == null)
            {
                throw new ArgumentNullException(nameof(buzzInCallback));
            }
            if (fiveSecondTimerExpiredCallback == null)
            {
                throw new ArgumentNullException(nameof(fiveSecondTimerExpiredCallback));
            }

            _buzzInCallback = buzzInCallback;
            _fiveSecondTimerExpiredCallback = fiveSecondTimerExpiredCallback;
        }

        /// <summary>
        /// Reset state machine to reset state of quiz box
        /// </summary>
        public void Reset()
        {
            _lastPaddle = null;
            _lastFiveSecondTimerExpired = false;
        }

        /// <summary>
        /// Process a new winner byte
        /// </summary>
        /// <param name="winnerByte">Winner byte</param>
        public void Process(StatusByte statusByte, WinnerByte winnerByte)
        {
            if (PaddleHelpers.TryParseWinnerByte(winnerByte, out var paddle))
            {
                var fiveSecondTimerExpired = winnerByte == WinnerByte.FIVE_SEC_TIMER_EXPIRED;

                if (paddle != _lastPaddle)
                {
                    if (paddle != null)
                    {
                        _buzzInCallback(paddle);
                    }

                    _lastPaddle = paddle;
                }

                if (fiveSecondTimerExpired != _lastFiveSecondTimerExpired)
                {
                    if (fiveSecondTimerExpired == true)
                    {
                        _fiveSecondTimerExpiredCallback();
                    }
                    _lastFiveSecondTimerExpired = fiveSecondTimerExpired;
                }
            }
        }
    }
}
