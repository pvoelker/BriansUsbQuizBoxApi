using System;

namespace BriansUsbQuizBoxApi
{
    /// <summary>
    /// Event arguments for a buzz in (paddle press) event
    /// </summary>
    public class BuzzInEventArgs : EventArgs
    {
        /// <summary>
        /// Paddle that was pressed
        /// </summary>
        public Paddle Paddle { get; private set; }        

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="paddle">Paddle that was pressed</param>
        public BuzzInEventArgs(Paddle paddle)
        {
            Paddle = paddle;
        }
    }
}
