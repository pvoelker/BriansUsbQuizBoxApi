using BriansUsbQuizBoxApi.Protocols;
using System;

namespace BriansUsbQuizBoxApi.Helpers
{
    /// <summary>
    /// Various command header helpers
    /// </summary>
    static internal class CommandHeaderHelpers
    {
        /// <summary>
        /// Returns whether the command header is expected to return a status report
        /// </summary>
        /// <param name="command">The command header to check</param>
        /// <returns>True if the command header is expected to return a status report, otherwise false</returns>
        static public bool IsStatusReturned(this CommandHeaderByte command)
        {
            if (command == CommandHeaderByte.START_REACTION_TIMING_GAME)
            {
                return true;
            }
            else if (command == CommandHeaderByte.START_5_SEC_TIMER)
            {
                return true;
            }
            else if (command == CommandHeaderByte.STATUS_REQUEST)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a function delegate that evaluates if a status byte is expected for a particular command header
        /// </summary>
        /// <param name="command">The command header to check</param>
        /// <returns>The delegate function</returns>
        /// <exception cref="InvalidOperationException">If the command header byte is not handled</exception>
        static public Func<StatusByte, bool> GetExpectedStatusLogic(this CommandHeaderByte command)
        {
            if (command == CommandHeaderByte.CLEAR)
            {
                return (x) => x == StatusByte.IDLE_MODE;
            }
            else if (command == CommandHeaderByte.START_REACTION_TIMING_GAME)
            {
                return (x) => x == StatusByte.GAME_PRESTART;
            }
            else if (command == CommandHeaderByte.START_5_SEC_TIMER)
            {
                return (x) => x == StatusByte.RUNNING_5_SEC_TIMER;
            }
            else if (command == CommandHeaderByte.START_30_SEC_TIMER)
            {
                return (x) => x == StatusByte.EXTENDED_TIMER_RUNNING;
            }
            else if (command == CommandHeaderByte.START_1_MIN_TIMER)
            {
                return (x) => x == StatusByte.EXTENDED_TIMER_RUNNING;
            }
            else if (command == CommandHeaderByte.START_2_MIN_TIMER)
            {
                return (x) => x == StatusByte.EXTENDED_TIMER_RUNNING;
            }
            else if (command == CommandHeaderByte.START_3_MIN_TIMER)
            {
                return (x) => x == StatusByte.EXTENDED_TIMER_RUNNING;
            }
            else if (command == CommandHeaderByte.START_INFINITE_TIMER)
            {
                return (x) => x == StatusByte.EXTENDED_TIMER_RUNNING;
            }
            else if (command == CommandHeaderByte.END_INFINITE_TIMER_BUZZ)
            {
                // There is no way to track the state change for this
                // After this command is sent, the state EXTENDED_TIMER_RUNNING sticks around for a bit
                // Resending the command sends the API into an infinite paddle lockout loop
                return (x) => true;
            }
            else if (command == CommandHeaderByte.STATUS_REQUEST)
            {
                return (x) => true; // Don't need to monitor state changes for status requests
            }
            else
            {
                throw new InvalidOperationException($"Command header byte '{command}' not handled");
            }
        }
    }
}
