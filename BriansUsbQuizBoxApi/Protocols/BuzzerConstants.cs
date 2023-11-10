using System;

namespace BriansUsbQuizBoxApi.Protocols
{
    /// <summary>
    /// Miscellaneous constants for quiz box HID reports
    /// </summary>
    public static class BuzzerConstants
    {
        /// <summary>
        /// Quiz box input and output report length
        /// </summary>
        public const int REPORT_LENGTH = 65;

        /// <summary>
        /// Quiz box game length in milliseconds
        /// </summary>
        public const decimal GAME_LENGTH = 1000;
    }
}
