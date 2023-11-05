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
        /// Converts the 2-byte value response time to a decimal in milliseconds
        /// </summary>
        /// <param name="data">The 2-byte value to convert</param>
        /// <returns>Response time in milliseconds</returns>
        /// <exception cref="ArgumentException">A 2-byte value was not provided</exception>
        static public decimal CalculateResponseTime(this byte[] data)
        {
            if (data == null || data.Length != 2)
            {
                throw new ArgumentException("Byte array must be of length 2", nameof(data));
            }

            // MSB is contained in the lower number byte of the two byte array
            var intData = BitConverter.ToUInt16(new byte[] { data[1], data[0] });

            return intData * COUNTER_INCREMENT_VALUE;
        }
    }
}
