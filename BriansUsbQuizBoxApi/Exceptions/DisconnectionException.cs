﻿using System;
using System.IO;

namespace BriansUsbQuizBoxApi.Exceptions
{
    /// <summary>
    /// Exception that is thrown when a disconnection is detected on a read/write.  Disconnection cleanup has occurred
    /// </summary>
    public class DisconnectionException : IOException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        public DisconnectionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor with inner exception
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public DisconnectionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
