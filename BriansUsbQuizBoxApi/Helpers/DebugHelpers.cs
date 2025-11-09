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
        /// Formatted debug write line with timestamp
        /// </summary>
        /// <param name="message">Message to write</param>
        [Conditional("DEBUG")]
        static public void WriteLine(string message)
        {
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " --- " + message);
        }
    }
}
