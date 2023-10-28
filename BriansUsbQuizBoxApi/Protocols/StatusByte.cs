using System;

namespace BriansUsbQuizBoxApi.Protocols
{
    /// <summary>
    /// Status byte values for quiz box HID reports
    /// </summary>
    public enum StatusByte : byte
    {
        /// <summary>
        /// Awaiting command or player to buzz in.
        /// </summary>
        IDLE_MODE = 0x00,

        /// <summary>
        /// The reaction time game has been started but the yellow light has not been turned on yet.
        /// </summary>
        GAME_PRESTART = 0x01,

        /// <summary>
        /// The reaction time game is running after the yellow light has started, but the game timer has not yet run out.
        /// </summary>
        GAME_RUNNING = 0x02,

        /// <summary>
        /// Someone has buzzed in and a timer is running to determine if other players are going to buzz in.
        /// </summary>
        PERSON_BUZZED_IN = 0x04,

        /// <summary>
        /// Either the reaction time game has finished its timer OR the player buzz in timer has finished.
        /// </summary>
        GAME_DONE = 0x08,

        /// <summary>
        /// This indicates that the 5 second timer is running and no one has buzzed in yet.
        /// </summary>
        RUNNING_5_SEC_TIMER = 0x10,

        /// <summary>
        /// This indicates that one of the timers of 30 seconds or longer is running.  During this state no one can buzz in. 
        /// </summary>
        EXTENDED_TIMER_RUNNING = 0x20,

        /// <summary>
        /// This is the status code when the quiz box is going through its LED and buzzer check at startup.
        /// </summary>
        STARTUP_SEQUENCING = 0x40
    }
}
