using System;

namespace BriansUsbQuizBoxApi.Protocols
{
    /// <summary>
    /// Command header byte values for quiz box HID reports
    /// </summary>
    public enum CommandHeaderByte
    {
        /// <summary>
        /// The clear command will clear all counters, LEDs, and any other quiz box variables.  This command can be used to exit any timer state or to get the box ready for its next task.
        /// </summary>
        CLEAR = 0x80,

        /// <summary>
        /// This command can only be started when the quiz box is in idle mode.
        /// </summary>
        START_REACTION_TIMING_GAME = 0x85,

        /// <summary>
        /// This command returns a status message from the quiz box.
        /// </summary>
        STATUS_REQUEST = 0x86,

        /// <summary>
        /// The start 5 second timer command gives the players 5 seconds to buzz in before they are locked out.  This command returns a status message.
        /// </summary>
        START_5_SEC_TIMER = 0x87,

        /// <summary>
        /// This command starts the extended 30 second timer and locks out the paddles until the timer has expired.
        /// </summary>
        START_30_SEC_TIMER = 0x88,

        /// <summary>
        /// This command starts the extended 1 minute timer and locks out the paddles until the timer has expired.
        /// </summary>
        START_1_MIN_TIMER = 0x89,

        /// <summary>
        /// This command starts the extended 2 minute timer and locks out the paddles until the timer has expired.
        /// </summary>
        START_2_MIN_TIMER = 0x8A,

        /// <summary>
        /// This command starts the extended 3 minute timer and locks out the paddles until the timer has expired.
        /// </summary>
        START_3_MIN_TIMER = 0x8B,

        /// <summary>
        /// This command starts a timer that will continuously run until the timer end command is sent.  This timer locks out the paddles just like the other timers.
        /// </summary>
        START_INFINITE_TIMER = 0x8C,

        /// <summary>
        /// This command ends the continuous timer in a similar method of the other timers.  If this command is sent without continuous timer running it will sound a buzz as the end of a timer.
        /// </summary>
        END_INFINITE_TIMER_BUZZ = 0x8D
    }
}
