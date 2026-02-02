using System;

namespace BriansUsbQuizBoxApi.Protocols
{
    /// <summary>
    /// Miscellaneous constants for quiz box HID reports
    /// </summary>
    internal static class BuzzerConstants
    {
        /// <summary>
        /// Quiz box input and output report length
        /// </summary>
        public const int REPORT_LENGTH = 65;

        /// <summary>
        /// Quiz box timer countdown length in milliseconds.  This is used for the reaction time game and tracking delta times from initial buzz ins (paddle presses)
        /// </summary>
        public const decimal TIMER_COUNTDOWN_LENGTH = 1000m;
    }
}
