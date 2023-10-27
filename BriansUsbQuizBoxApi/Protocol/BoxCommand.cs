using System;

namespace BriansUsbQuizBoxApi.Protocol
{
    /// <summary>
    /// Quiz box command output HID report
    /// </summary>
    public class BoxCommand
    {
        public CommandHeaderByte CommandHeader { get; set; }

        public byte[] BuildByteArray()
        {
            if (CommandHeader == 0)
            {
                throw new InvalidOperationException($"{nameof(CommandHeader)} cannot be zero");
            }

            var retVal = new byte[BuzzerConstants.REPORT_LENGTH];

            retVal[1] = (byte)CommandHeader;

            return retVal;
        }
    }
}
