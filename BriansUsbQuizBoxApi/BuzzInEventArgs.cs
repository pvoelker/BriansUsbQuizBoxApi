using System;

namespace BriansUsbQuizBoxApi
{
    /// <summary>
    /// Event arguments for a buzz in (paddle press) event
    /// </summary>
    public class BuzzInEventArgs : EventArgs
    {
        /// <summary>
        /// Paddle color that was pressed
        /// </summary>
        public PaddleColorEnum PaddleColor { get; private set; }        

        /// <summary>
        /// Paddle number that was pressed
        /// </summary>
        public PaddleNumberEnum PaddleNumber {  get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="paddleColor">Paddle color that was pressed</param>
        /// <param name="paddleNumber">Paddle number that was pressed</param>
        public BuzzInEventArgs(PaddleColorEnum paddleColor, PaddleNumberEnum paddleNumber)
        {
            PaddleColor = paddleColor;
            PaddleNumber = paddleNumber;
        }
    }
}
