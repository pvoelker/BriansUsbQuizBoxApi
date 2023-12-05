using System;

namespace BriansUsbQuizBoxApi
{
    /// <summary>
    /// Paddle lockout timer enumerations
    /// </summary>
    public enum LockoutTimerEnum
    {
        /// <summary>
        /// 30 second paddle lockout timer
        /// </summary>
        Timer30Seconds = 1,
        /// <summary>
        /// 1 minute paddle lockout timer
        /// </summary>
        Timer1Minute = 2,
        /// <summary>
        /// 2 minute paddle lockout timer
        /// </summary>
        Timer2Minutes = 3,
        /// <summary>
        /// 3 minute paddle lockout timer
        /// </summary>
        Timer3Minutes = 4
    }
}
