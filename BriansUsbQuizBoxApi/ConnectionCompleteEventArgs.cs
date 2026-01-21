using System;

namespace BriansUsbQuizBoxApi
{
    /// <summary>
    /// Event arguments for a connection complete event
    /// </summary>
    public class ConnectionCompleteEventArgs : EventArgs
    {
        /// <summary>
        /// Communication protocol version for the connected device
        /// </summary>
        public byte ProtocolVersion { get; private set; }

        /// <summary>
        /// True if additional winner information (beyond first place winner) is included, otherwise false
        /// </summary>
        public bool HasAdditionalWinnerInfo { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="protocolVersion">Communication protocol version</param>
        /// <param name="hasAdditionalWinnerInfo">Indicates if additional winner information is provided (beyond first place winner)</param>
        public ConnectionCompleteEventArgs(byte protocolVersion, bool hasAdditionalWinnerInfo)
        {
            ProtocolVersion = protocolVersion;
            HasAdditionalWinnerInfo = hasAdditionalWinnerInfo;
        }
    }
}
