using System;

namespace BriansUsbQuizBoxApi
{
    /// <summary>
    /// Event arguments for when a reaction game completes.  Maximum reaction time is 1000ms
    /// </summary>
    public class GameDoneEventArgs : EventArgs
    {
        /// <summary>
        /// First place winner paddle that was pressed. Null if no winner
        /// </summary>
        public Paddle? Winner { get; private set; }

        /// <summary>
        /// True if additional winner information (beyond first place winner) is included, otherwise false
        /// </summary>
        public bool HasAdditionalWinnerInfo { get; private set; }

        /// <summary>
        /// Second place winner paddle that was pressed. Null if no winner
        /// </summary>
        public Paddle? Winner2 { get; private set; }

        /// <summary>
        /// Third place winner paddle that was pressed. Null if no winner
        /// </summary>
        public Paddle? Winner3 { get; private set; }

        /// <summary>
        /// Fourth place winner paddle that was pressed. Null if no winner
        /// </summary>
        public Paddle? Winner4 { get; private set; }

        /// <summary>
        /// Fifth place winner paddle that was pressed. Null if no winner
        /// </summary>
        public Paddle? Winner5 { get; private set; }

        /// <summary>
        /// Sixth place winner paddle that was pressed. Null if no winner
        /// </summary>
        public Paddle? Winner6 { get; private set; }

        /// <summary>
        /// Seventh place winner paddle that was pressed. Null if no winner
        /// </summary>
        public Paddle? Winner7 { get; private set; }

        /// <summary>
        /// Eigth place winner paddle that was pressed. Null if no winner
        /// </summary>
        public Paddle? Winner8 { get; private set; }

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
        /// <param name="additionalWinnerInfo">True if additional winner info (beyond first place winner) is included, other false</param>
        /// <param name="winner1">First place winner paddle that was pressed. Null if no winner</param>
        /// <param name="winner2">Second winner paddle that was pressed. Null if no winner</param>
        /// <param name="winner3">Third winner paddle that was pressed. Null if no winner</param>
        /// <param name="winner4">Fourth winner paddle that was pressed. Null if no winner</param>
        /// <param name="winner5">Fifth winner paddle that was pressed. Null if no winner</param>
        /// <param name="winner6">Sixth winner paddle that was pressed. Null if no winner</param>
        /// <param name="winner7">Seventh winner paddle that was pressed. Null if no winner</param>
        /// <param name="winner8">Eight winner paddle that was pressed. Null if no winner</param>
        /// <param name="red1Time">Reaction time for red 1 paddle in milliseconds. Null if no buzz in (paddle press) for red 1 paddle</param>
        /// <param name="red2Time">Reaction time for red 2 paddle in milliseconds. Null if no buzz in (paddle press) for red 2 paddle</param>
        /// <param name="red3Time">Reaction time for red 3 paddle in milliseconds. Null if no buzz in (paddle press) for red 3 paddle</param>
        /// <param name="red4Time">Reaction time for red 4 paddle in milliseconds. Null if no buzz in (paddle press) for red 4 paddle</param>
        /// <param name="green1Time">Reaction time for green 1 paddle in milliseconds. Null if no buzz in (paddle press) for green 1 paddle</param>
        /// <param name="green2Time">Reaction time for green 2 paddle in milliseconds. Null if no buzz in (paddle press) for green 2 paddle</param>
        /// <param name="green3Time">Reaction time for green 3 paddle in milliseconds. Null if no buzz in (paddle press) for green 3 paddle</param>
        /// <param name="green4Time">Reaction time for green 4 paddle in milliseconds. Null if no buzz in (paddle press) for green 4 paddle</param>
        public GameDoneEventArgs(bool additionalWinnerInfo,
            Paddle? winner1, Paddle? winner2, Paddle? winner3, Paddle? winner4,
            Paddle? winner5, Paddle? winner6, Paddle? winner7, Paddle? winner8,
            decimal? red1Time, decimal? red2Time, decimal? red3Time, decimal? red4Time,
            decimal? green1Time, decimal? green2Time, decimal? green3Time, decimal? green4Time)
        {
            HasAdditionalWinnerInfo = additionalWinnerInfo;
            Winner = winner1;
            Winner2 = winner2;
            Winner3 = winner3;
            Winner4 = winner4;
            Winner5 = winner5;
            Winner6 = winner6;
            Winner7 = winner7;
            Winner8 = winner8;
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
