using BriansUsbQuizBoxApi.Protocols;
using System;

namespace BriansUsbQuizBoxApi.Helpers
{
    /// <summary>
    /// Various time related helpers
    /// </summary>
    static public class TimeHelpers
    {
        static readonly private decimal COUNTER_INCREMENT_VALUE = 1.02m;

        /// <summary>
        /// Checks if the 2-byte value response time is zero
        /// </summary>
        /// <param name="data">The 2-byte value to convert</param>
        /// <returns>True if the response time is zero, false if greater than zero</returns>
        /// <exception cref="ArgumentException">A 2-byte value was not provided</exception>
        static public bool IsResponseTimeZero(this byte[] data)
        {
            if (data == null || data.Length != 2)
            {
                throw new ArgumentException("Byte array must be of length 2", nameof(data));
            }

            if (data[0] == 0 && data[1] == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Converts the 2-byte value response time to a decimal in milliseconds
        /// </summary>
        /// <param name="data">The 2-byte value to convert</param>
        /// <returns>Response time in milliseconds or null if no buzz in (paddle press) within 1 second timer</returns>
        /// <exception cref="ArgumentException">A 2-byte value was not provided</exception>
        static public decimal? CalculateResponseTime(this byte[] data)
        {
            if (data == null || data.Length != 2)
            {
                throw new ArgumentException("Byte array must be of length 2", nameof(data));
            }

            if (IsResponseTimeZero(data))
            {
                return null;
            }
            else
            {
                // MSB is contained in the lower number byte of the two byte array
                var intData = BitConverter.ToUInt16(new byte[] { data[1], data[0] });

                // This value is the countdown value on the 1 second (1000ms) timer, so to get response time we need to subtract this value from 1000
                var timerCountDown = intData * COUNTER_INCREMENT_VALUE;

                // The countdown time can come back as 1000.62, which will make the response time -0.62.  So cap the minimum response time value at 0
                return Math.Max(0, BuzzerConstants.TIMER_COUNTDOWN_LENGTH - timerCountDown);
            }
        }
    }
}
