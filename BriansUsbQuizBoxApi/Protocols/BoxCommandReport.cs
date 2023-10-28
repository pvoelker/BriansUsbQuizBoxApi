using System;

namespace BriansUsbQuizBoxApi.Protocols
{
    /// <summary>
    /// Quiz box command output HID report
    /// </summary>
    public class BoxCommandReport
    {
        public CommandHeaderByte CommandHeader { get; set; }

        public byte[] BuildByteArray()
        {
            if (CommandHeader == 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(CommandHeader)} cannot be zero");
            }

            var retVal = new byte[BuzzerConstants.REPORT_LENGTH];

            retVal[1] = (byte)CommandHeader;

            return retVal;
        }
    }
}
