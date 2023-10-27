using System;
using System.Collections.Generic;
using System.Text;

namespace BriansUsbQuizBoxApi.Exceptions
{
    /// <summary>
    /// Exception that is thrown when a quiz box operation is attempted without connecting
    /// </summary>
    public class NotConnectedException : InvalidOperationException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        public NotConnectedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor with inner exception
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public NotConnectedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
