using System;

namespace BriansUsbQuizBoxApi.Protocol
{
    /// <summary>
    /// Winner byte values for quiz box HID reports
    /// </summary>
    public enum WinnerByte : byte
    {
        /// <summary>
        /// No valid winner.
        /// </summary>
        NO_VALID_WINNER = 0x00,

        /// <summary>
        /// The 5 second timer has expired.
        /// </summary>
        FIVE_SEC_TIMER_EXPIRED = 0x01,

        /// <summary>
        /// Red 4 buzzed in.
        /// </summary>
        RED_4 = 0x03,

        /// <summary>
        /// Red 3 buzzed in.
        /// </summary>
        RED_3 = 0x04,

        /// <summary>
        /// Red 2 buzzed in.
        /// </summary>
        RED_2 = 0x05,

        /// <summary>
        /// Red 1 buzzed in.
        /// </summary>
        RED_1 = 0x06,

        /// <summary>
        /// Green 1 buzzed in.
        /// </summary>
        GREEN_1 = 0x07,

        /// <summary>
        /// Green 2 buzzed in.
        /// </summary>
        GREEN_2 = 0x08,

        /// <summary>
        /// Green 3 buzzed in.
        /// </summary>
        GREEN_3 = 0x09,

        /// <summary>
        /// Green 4 buzzed in.
        /// </summary>
        GREEN_4 = 0x0A
    }
}
