using System;

namespace BriansUsbQuizBoxApi.Exceptions
{
    /// <summary>
    /// Exception that is thrown when more than one quiz box is detected.  Only one quiz box can be connected at a time
    /// </summary>
    public class MultipleDevicesException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        public MultipleDevicesException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor with inner exception
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public MultipleDevicesException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
