using BriansUsbQuizBoxApi.Protocols;
using System;

namespace BriansUsbQuizBoxApi.Helpers
{
    /// <summary>
    /// Various paddle winner byte related helpers
    /// </summary>
    static public class PaddleHelpers
    {
        /// <summary>
        /// Convert a paddle winner byte to a paddle number and color.  Default values are returned for 5 second timer expiration and no valid winner
        /// </summary>
        /// <param name="winnerByte">The winner byte to parse</param>
        /// <param name="paddleNumber">The paddle number for the winner byte</param>
        /// <param name="paddleColor">The paddle color for the winner byte</param>
        /// <returns>True if the winner byte was successfully parsed</returns>
        static public bool TryParseWinnerByte(WinnerByte winnerByte, out Paddle? paddle)
        {
            paddle = null;

            switch (winnerByte)
            {
                case WinnerByte.FIVE_SEC_TIMER_EXPIRED:
                    return true;
                case WinnerByte.NO_VALID_WINNER:
                    return true;
                case WinnerByte.RED_1:
                    paddle = Paddle.RED_1;
                    return true;
                case WinnerByte.RED_2:
                    paddle = Paddle.RED_2;
                    return true;
                case WinnerByte.RED_3:
                    paddle = Paddle.RED_3;
                    return true;
                case WinnerByte.RED_4:
                    paddle = Paddle.RED_4;
                    return true;
                case WinnerByte.GREEN_1:
                    paddle = Paddle.GREEN_1;
                    return true;
                case WinnerByte.GREEN_2:
                    paddle = Paddle.GREEN_2;
                    return true;
                case WinnerByte.GREEN_3:
                    paddle = Paddle.GREEN_3;
                    return true;
                case WinnerByte.GREEN_4:
                    paddle = Paddle.GREEN_4;
                    return true;
            }

            return false;
        }
    }
}
