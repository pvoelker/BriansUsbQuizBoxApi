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
        /// Constructor
        /// </summary>
        /// <param name="protocolVersion">Communication protocol version</param>
        public ConnectionCompleteEventArgs(byte protocolVersion)
        {
            ProtocolVersion = protocolVersion;
        }
    }
}
