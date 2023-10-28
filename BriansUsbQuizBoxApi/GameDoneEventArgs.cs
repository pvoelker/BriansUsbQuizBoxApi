using BriansUsbQuizBoxApi.Protocols;
using System;

namespace BriansUsbQuizBoxApi
{
    /// <summary>
    /// Event arguments for when a game completes
    /// </summary>
    public class GameDoneEventArgs : EventArgs
    {
        /// <summary>
        /// Game time for red 1 paddle
        /// </summary>
        public decimal Red1Time { get; private set; }

        /// <summary>
        /// Game time for red 2 paddle
        /// </summary>
        public decimal Red2Time { get; private set; }

        /// <summary>
        /// Game time for red 3 paddle
        /// </summary>
        public decimal Red3Time { get; private set; }

        /// <summary>
        /// Game time for red 4 paddle
        /// </summary>
        public decimal Red4Time { get; private set; }

        /// <summary>
        /// Game time for green 1 paddle
        /// </summary>
        public decimal Green1Time { get; private set; }

        /// <summary>
        /// Game time for green 2 paddle
        /// </summary>
        public decimal Green2Time { get; private set; }

        /// <summary>
        /// Game time for green 3 paddle
        /// </summary>
        public decimal Green3Time { get; private set; }

        /// <summary>
        /// Game time for green 4 paddle
        /// </summary>
        public decimal Green4Time { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="red1Time">Time for red 1 paddle</param>
        /// <param name="red2Time">Time for red 2 paddle</param>
        /// <param name="red3Time">Time for red 3 paddle</param>
        /// <param name="red4Time">Time for red 4 paddle</param>
        /// <param name="green1Time">Time for green 1 paddle</param>
        /// <param name="green2Time">Time for green 2 paddle</param>
        /// <param name="green3Time">Time for green 3 paddle</param>
        /// <param name="green4Time">Time for green 4 paddle</param>
        public GameDoneEventArgs(decimal red1Time, decimal red2Time, decimal red3Time, decimal red4Time,
            decimal green1Time, decimal green2Time, decimal green3Time, decimal green4Time)
        {
            Red1Time = red1Time;
            Red2Time = red2Time;
            Red3Time = red3Time;
            Red4Time = red4Time;
            Green1Time = green1Time;
            Green2Time = green2Time;
            Green3Time = green3Time;
            Green4Time = green4Time;
        }
    }
}
