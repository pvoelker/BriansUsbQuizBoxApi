using System;
using BriansUsbQuizBoxApi.Helpers;

namespace BriansUsbQuizBoxApi.Protocols
{
    /// <summary>
    /// Quiz box status input HID report
    /// </summary>
    public class BoxStatusReport : IEquatable<BoxStatusReport>
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

        /// <summary>
        /// Object check for equality
        /// </summary>
        /// <param name="obj">Object to check against</param>
        /// <returns>True if equal, otherwise false</returns>
        public override bool Equals(object obj) => this.Equals(obj as BoxStatusReport);

        /// <summary>
        /// Check for equality
        /// </summary>
        /// <param name="p">Object to check against</param>
        /// <returns>True if equal, otherwise false</returns>
        public bool Equals(BoxStatusReport? p)
        {
            if (p is null)
            {
                return false;
            }

            if (Object.ReferenceEquals(this, p))
            {
                return true;
            }

            return Status == p.Status &&
                Winner == p.Winner &&
                Red1Time == p.Red1Time &&
                Red2Time == p.Red2Time &&
                Red3Time == p.Red3Time &&
                Red4Time == p.Red4Time &&
                Green1Time == p.Green1Time &&
                Green2Time == p.Green2Time &&
                Green3Time == p.Green3Time &&
                Green4Time == p.Green4Time;
        }

        /// <summary>
        /// Generate hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode() => (Status, Winner, Red1Time, Red2Time, Red3Time, Red4Time,
            Green1Time, Green2Time, Green3Time, Green4Time).GetHashCode();

        /// <summary>
        /// Operator '==' (equal)
        /// </summary>
        /// <param name="lhs">Left hand side to check</param>
        /// <param name="rhs">Right hand side to check</param>
        /// <returns>True if equal, otherwise false</returns>
        public static bool operator==(BoxStatusReport? lhs, BoxStatusReport? rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }

            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Operator '!=' (not equal)
        /// </summary>
        /// <param name="lhs">Left hand side to check</param>
        /// <param name="rhs">Right hand side to check</param>
        /// <returns>True if not equal, otherwise false</returns>
        public static bool operator!=(BoxStatusReport? lhs, BoxStatusReport? rhs) => !(lhs == rhs);

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
            return $"Status: {Status} (0x{Status:X}); Winner: {Winner} (0x{Winner:X}); "
                + $"Red 1 Time: {(Red1Time.HasValue ? Red1Time.Value.ToString() + "ms" : "[None]")}; "
                + $"Red 2 Time: {(Red2Time.HasValue ? Red2Time.Value.ToString() + "ms" : "[None]")}; "
                + $"Red 3 Time: {(Red3Time.HasValue ? Red3Time.Value.ToString() + "ms" : "[None]")}; "
                + $"Red 4 Time: {(Red4Time.HasValue ? Red4Time.Value.ToString() + "ms" : "[None]")}; "
                + $"Green 1 Time: {(Green1Time.HasValue ? Green1Time.Value.ToString() + "ms" : "[None]")}; "
                + $"Green 2 Time: {(Green2Time.HasValue ? Green2Time.Value.ToString() + "ms" : "[None]")}; "
                + $"Green 3 Time: {(Green3Time.HasValue ? Green3Time.Value.ToString() + "ms" : "[None]")}; "
                + $"Green 4 Time: {(Green4Time.HasValue ? Green4Time.Value.ToString() + "ms" : "[None]")}";
        }
    }
}
