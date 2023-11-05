using System;

namespace BriansUsbQuizBoxApi
{
    /// <summary>
    /// Event arguments for a disconnection event
    /// </summary>
    public class DisconnectionEventArgs : EventArgs
    {
        /// <summary>
        /// Exception that caused the disconnection
        /// </summary>
        public Exception Exception { get; private set; }        

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="exception">Exception that caused the disconnection</param>
        public DisconnectionEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}
