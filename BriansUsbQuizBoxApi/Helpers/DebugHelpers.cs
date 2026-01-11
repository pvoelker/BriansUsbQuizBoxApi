using System;
using System.Diagnostics;

namespace BriansUsbQuizBoxApi.Helpers
{
    /// <summary>
    /// Various debugging helpers
    /// </summary>
    static public class DebugHelpers
    {
        /// <summary>
        /// Debug write line with timestamp
        /// </summary>
        /// <param name="message">Message to write</param>
        [Conditional("DEBUG")]
        static public void WriteLine(string message)
        {
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " --- " + message);
        }

        /// <summary>
        /// Formatted debug write line with timestamp
        /// </summary>
        /// <param name="format">Message to format and write</param>
        /// <param name="args">Message format arguments</param>
        [Conditional("DEBUG")]
        static public void WriteLine(string format, params object[] args)
        {
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " --- " + format, args);
        }
    }
}
