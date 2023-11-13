using System;
using System.Runtime.CompilerServices;
using BriansUsbQuizBoxApi.Helpers;

namespace BriansUsbQuizBoxApi.Protocols
{
    /// <summary>
    /// Quiz box status input HID report
    /// </summary>
    public class BoxStatusReport
    {
        public StatusByte Status { get; private set; }

        public WinnerByte Winner { get; private set; }

        public decimal? Red1Time { get; private set; }

        public decimal? Red2Time { get; private set; }

        public decimal? Red3Time { get; private set; }

        public decimal? Red4Time { get; private set; }

        public decimal? Green1Time { get; private set; }

        public decimal? Green2Time { get; private set; }

        public decimal? Green3Time { get; private set; }

        public decimal? Green4Time { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public BoxStatusReport()
        {
        }

        /// <summary>
        /// Constructor for testing purposes only
        /// </summary>
        /// <param name="status">Status byte</param>
        /// <param name="winner">Winner byte</param>
        /// <param name="red1Time">Time for red 1 paddle or null for no buzz in (paddle press)</param>
        /// <param name="red2Time">Time for red 2 paddle or null for no buzz in (paddle press)</param>
        /// <param name="red3Time">Time for red 3 paddle or null for no buzz in (paddle press)</param>
        /// <param name="red4Time">Time for red 4 paddle or null for no buzz in (paddle press)</param>
        /// <param name="green1Time">Time for green 1 paddle or null for no buzz in (paddle press)</param>
        /// <param name="green2Time">Time for green 2 paddle or null for no buzz in (paddle press)</param>
        /// <param name="green3Time">Time for green 3 paddle or null for no buzz in (paddle press)</param>
        /// <param name="green4Time">Time for green 4 paddle or null for no buzz in (paddle press)</param>
        public BoxStatusReport(StatusByte status, WinnerByte winner,
            decimal? red1Time, decimal? red2Time, decimal? red3Time, decimal? red4Time,
            decimal? green1Time, decimal? green2Time, decimal? green3Time, decimal? green4Time)
        {
            Status = status;
            Winner = winner;
            Red1Time = red1Time;
            Red2Time = red2Time;
            Red3Time = red3Time;
            Red4Time = red4Time;
            Green1Time = green1Time;
            Green2Time = green2Time;
            Green3Time = green3Time;
            Green4Time = green4Time;
        }

        public static BoxStatusReport Parse(byte[] data)
        {
            if (data.Length != BuzzerConstants.REPORT_LENGTH)
            {
                throw new ArgumentOutOfRangeException(nameof(data), $"Array expected to be {BuzzerConstants.REPORT_LENGTH} bytes in length");
            }
            if (data[0] != 0x00)
            {
                throw new ArgumentOutOfRangeException(nameof(data), "First byte expected to be 0x00");
            }
            if (data[1] != 0x00)
            {
                throw new ArgumentOutOfRangeException(nameof(data), "Second byte expected to be 0x00");
            }

            var retVal = new BoxStatusReport
            {
                Status = (StatusByte)data[2],
                Winner = (WinnerByte)data[3],
                Red1Time = new byte[] { data[4], data[5] }.CalculateResponseTime(),
                Red2Time = new byte[] { data[6], data[7] }.CalculateResponseTime(),
                Red3Time = new byte[] { data[8], data[9] }.CalculateResponseTime(),
                Red4Time = new byte[] { data[10], data[11] }.CalculateResponseTime(),
                Green1Time = new byte[] { data[12], data[13] }.CalculateResponseTime(),
                Green2Time = new byte[] { data[14], data[15] }.CalculateResponseTime(),
                Green3Time = new byte[] { data[16], data[17] }.CalculateResponseTime(),
                Green4Time = new byte[] { data[18], data[19] }.CalculateResponseTime()
            };

            // Bytes 20-64 are don't cares...

            return retVal;
        }

        public override string ToString()
        {
            return $"Status: {Status}; Winner: {Winner}" + Environment.NewLine
                + $"Red 1 Time: {Red1Time}; Red 2 Time: {Red2Time}; Red 3 Time: {Red3Time}; Red 4 Time: {Red4Time}" + Environment.NewLine
                + $"Green 1 Time: {Green1Time}; Green 2 Time: {Green2Time}; Green 3 Time: {Green3Time}; Green 4 Time: {Green4Time}";
        }
    }
}
