﻿using BriansUsbQuizBoxApi.Protocols;
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
        static public bool TryParseWinnerByte(WinnerByte winnerByte, out PaddleNumberEnum paddleNumber, out PaddleColorEnum paddleColor)
        {
            paddleNumber = PaddleNumberEnum.None;
            paddleColor = PaddleColorEnum.None;

            switch (winnerByte)
            {
                case WinnerByte.FIVE_SEC_TIMER_EXPIRED:
                    paddleNumber = PaddleNumberEnum.None;
                    paddleColor = PaddleColorEnum.None;
                    return true;
                case WinnerByte.NO_VALID_WINNER:
                    paddleNumber = PaddleNumberEnum.None;
                    paddleColor = PaddleColorEnum.None;
                    return true;
                case WinnerByte.RED_1:
                    paddleNumber = PaddleNumberEnum.Paddle1;
                    paddleColor = PaddleColorEnum.Red;
                    return true;
                case WinnerByte.RED_2:
                    paddleNumber = PaddleNumberEnum.Paddle2;
                    paddleColor = PaddleColorEnum.Red;
                    return true;
                case WinnerByte.RED_3:
                    paddleNumber = PaddleNumberEnum.Paddle3;
                    paddleColor = PaddleColorEnum.Red;
                    return true;
                case WinnerByte.RED_4:
                    paddleNumber = PaddleNumberEnum.Paddle4;
                    paddleColor = PaddleColorEnum.Red;
                    return true;
                case WinnerByte.GREEN_1:
                    paddleNumber = PaddleNumberEnum.Paddle1;
                    paddleColor = PaddleColorEnum.Green;
                    return true;
                case WinnerByte.GREEN_2:
                    paddleNumber = PaddleNumberEnum.Paddle2;
                    paddleColor = PaddleColorEnum.Green;
                    return true;
                case WinnerByte.GREEN_3:
                    paddleNumber = PaddleNumberEnum.Paddle3;
                    paddleColor = PaddleColorEnum.Green;
                    return true;
                case WinnerByte.GREEN_4:
                    paddleNumber = PaddleNumberEnum.Paddle4;
                    paddleColor = PaddleColorEnum.Green;
                    return true;
            }

            return false;
        }
    }
}
