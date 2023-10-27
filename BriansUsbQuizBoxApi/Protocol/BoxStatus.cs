using System;
using BriansUsbQuizBoxApi.Helpers;

namespace BriansUsbQuizBoxApi.Protocol
{
    /// <summary>
    /// Quiz box status input HID report
    /// </summary>
    public class BoxStatus
    {
        public StatusByte Status { get; set; }

        public WinnerByte Winner { get; set; }

        public decimal Red1Time { get; set; }

        public decimal Red2Time { get; set; }

        public decimal Red3Time { get; set; }

        public decimal Red4Time { get; set; }

        public decimal Green1Time { get; set; }

        public decimal Green2Time { get; set; }

        public decimal Green3Time { get; set; }

        public decimal Green4Time { get; set; }

        public static BoxStatus Parse(byte[] data)
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

            var retVal = new BoxStatus
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
