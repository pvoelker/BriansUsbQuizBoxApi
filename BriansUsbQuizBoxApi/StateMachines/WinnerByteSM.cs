using BriansUsbQuizBoxApi.Helpers;
using BriansUsbQuizBoxApi.Protocols;
using System;

namespace BriansUsbQuizBoxApi.StateMachines
{
    public delegate void BuzzInCallback(PaddleColorEnum paddleColor, int paddleNumber);

    public delegate void FiveSecondTimerExpiredCallback();

    /// <summary>
    /// Winner Byte state machine
    /// </summary>
    public class WinnerByteSM
    {
        private BuzzInCallback _buzzInCallback;
        private FiveSecondTimerExpiredCallback _fiveSecondTimerExpiredCallback;

        private int _lastPaddleNumber = 0;
        private PaddleColorEnum _lastPaddleColor = PaddleColorEnum.None;
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
            _lastPaddleNumber = 0;
            _lastPaddleColor = PaddleColorEnum.None;
            _lastFiveSecondTimerExpired = false;
        }

        /// <summary>
        /// Process a new winner byte
        /// </summary>
        /// <param name="winnerByte">Winner byte</param>
        public void Process(StatusByte statusByte, WinnerByte winnerByte)
        {
            if (statusByte == StatusByte.IDLE_MODE)
            {
                Reset();
            }

            if (PaddleHelpers.TryParseWinnerByte(winnerByte, out var paddleNumber, out var paddleColor))
            {
                var fiveSecondTimerExpired = winnerByte == WinnerByte.FIVE_SEC_TIMER_EXPIRED;

                if (paddleNumber != _lastPaddleNumber || paddleColor != _lastPaddleColor)
                {
                    if (paddleColor != PaddleColorEnum.None)
                    {
                        _buzzInCallback(paddleColor, paddleNumber);
                    }

                    _lastPaddleNumber = paddleNumber;
                    _lastPaddleColor = paddleColor;
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
