using BriansUsbQuizBoxApi.Helpers;
using BriansUsbQuizBoxApi.Protocols;
using System;

namespace BriansUsbQuizBoxApi.StateMachines
{
    internal delegate void BuzzInCallback(Paddle paddle);

    internal delegate void FiveSecondTimerExpiredCallback();

    /// <summary>
    /// Winner Byte state machine
    /// </summary>
    internal class WinnerByteSM
    {
        private readonly BuzzInCallback _buzzInCallback;
        private readonly FiveSecondTimerExpiredCallback _fiveSecondTimerExpiredCallback;

        private Paddle? _lastPaddle = null;
        private bool _bLastFiveSecondTimerExpired = false;

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
            _buzzInCallback = buzzInCallback ?? throw new ArgumentNullException(nameof(buzzInCallback));
            _fiveSecondTimerExpiredCallback = fiveSecondTimerExpiredCallback ?? throw new ArgumentNullException(nameof(fiveSecondTimerExpiredCallback));
        }

        /// <summary>
        /// Reset state machine to reset state of quiz box
        /// </summary>
        public void Reset()
        {
            _lastPaddle = null;
            _bLastFiveSecondTimerExpired = false;
        }

        /// <summary>
        /// Process a new winner byte
        /// </summary>
        /// <param name="status">Box status report</param>
        public void Process(BoxStatusReport status)
        {
            if (PaddleHelpers.TryParseWinnerByte(status.Winner, out var paddle1)
                && PaddleHelpers.TryParseWinnerByte(status.Winner2, out var paddle2)
                && PaddleHelpers.TryParseWinnerByte(status.Winner3, out var paddle3)
                && PaddleHelpers.TryParseWinnerByte(status.Winner4, out var paddle4)
                && PaddleHelpers.TryParseWinnerByte(status.Winner5, out var paddle5)
                && PaddleHelpers.TryParseWinnerByte(status.Winner6, out var paddle6)
                && PaddleHelpers.TryParseWinnerByte(status.Winner7, out var paddle7)
                && PaddleHelpers.TryParseWinnerByte(status.Winner8, out var paddle8))
            {
                var bFiveSecondTimerExpired = status.Winner == WinnerByte.FIVE_SEC_TIMER_EXPIRED;

                if (paddle1 != _lastPaddle)
                {
                    if (paddle1 != null)
                    {
                        _buzzInCallback(paddle1);
                    }

                    _lastPaddle = paddle1;
                }

                if (bFiveSecondTimerExpired != _bLastFiveSecondTimerExpired)
                {
                    if (bFiveSecondTimerExpired == true)
                    {
                        _fiveSecondTimerExpiredCallback();
                    }
                    _bLastFiveSecondTimerExpired = bFiveSecondTimerExpired;
                }
            }
        }
    }
}
