using System;

namespace BriansUsbQuizBoxApi
{
    /// <summary>
    /// Event arguments for when a reaction game completes.  Maximum reaction time is 1000ms
    /// </summary>
    public class GameDoneEventArgs : EventArgs
    {
        /// <summary>
        /// Winner paddle that was pressed. Null if no winner
        /// </summary>
        public Paddle? Winner { get; private set; }

        /// <summary>
        /// Reaction time for red 1 paddle in milliseconds. Null if no buzz in (paddle press) for red 1 paddle
        /// </summary>
        public decimal? Red1Time { get; private set; }

        /// <summary>
        /// Reaction time for red 2 paddle in milliseconds. Null if no buzz in (paddle press) for red 2 paddle
        /// </summary>
        public decimal? Red2Time { get; private set; }

        /// <summary>
        /// Reaction time for red 3 paddle in milliseconds. Null if no buzz in (paddle press) for red 3 paddle
        /// </summary>
        public decimal? Red3Time { get; private set; }

        /// <summary>
        /// Reaction time for red 4 paddle in milliseconds. Null if no buzz in (paddle press) for red 4 paddle
        /// </summary>
        public decimal? Red4Time { get; private set; }

        /// <summary>
        /// Reaction time for green 1 paddle in milliseconds. Null if no buzz in (paddle press) for green 1 paddle
        /// </summary>
        public decimal? Green1Time { get; private set; }

        /// <summary>
        /// Reaction time for green 2 paddle in milliseconds. Null if no buzz in (paddle press) for green 2 paddle
        /// </summary>
        public decimal? Green2Time { get; private set; }

        /// <summary>
        /// Reaction time for green 3 paddle in milliseconds. Null if no buzz in (paddle press) for green 3 paddle
        /// </summary>
        public decimal? Green3Time { get; private set; }

        /// <summary>
        /// Reaction time for green 4 paddle in milliseconds. Null if no buzz in (paddle press) for green 4 paddle
        /// </summary>
        public decimal? Green4Time { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <para name="winner">Winner paddle that was pressed. Null if no winner</para>
        /// <param name="red1Time">Reaction time for red 1 paddle in milliseconds. Null if no buzz in (paddle press) for red 1 paddle</param>
        /// <param name="red2Time">Reaction time for red 2 paddle in milliseconds. Null if no buzz in (paddle press) for red 2 paddle</param>
        /// <param name="red3Time">Reaction time for red 3 paddle in milliseconds. Null if no buzz in (paddle press) for red 3 paddle</param>
        /// <param name="red4Time">Reaction time for red 4 paddle in milliseconds. Null if no buzz in (paddle press) for red 4 paddle</param>
        /// <param name="green1Time">Reaction time for green 1 paddle in milliseconds. Null if no buzz in (paddle press) for green 1 paddle</param>
        /// <param name="green2Time">Reaction time for green 2 paddle in milliseconds. Null if no buzz in (paddle press) for green 2 paddle</param>
        /// <param name="green3Time">Reaction time for green 3 paddle in milliseconds. Null if no buzz in (paddle press) for green 3 paddle</param>
        /// <param name="green4Time">Reaction time for green 4 paddle in milliseconds. Null if no buzz in (paddle press) for green 4 paddle</param>
        public GameDoneEventArgs(Paddle? winner,
            decimal? red1Time, decimal? red2Time, decimal? red3Time, decimal? red4Time,
            decimal? green1Time, decimal? green2Time, decimal? green3Time, decimal? green4Time)
        {
            Winner = winner;
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
