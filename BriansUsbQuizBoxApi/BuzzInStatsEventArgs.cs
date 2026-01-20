using System;

namespace BriansUsbQuizBoxApi
{
    /// <summary>
    /// Event arguments for when buzz in statistics are sent by the quiz box.  Typically after a 5 second timer or one second after an initial buzz in (paddle press)
    /// </summary>
    public class BuzzInStatsEventArgs : EventArgs
    {
        /// <summary>
        /// First place winner paddle that was pressed. Null is no winner
        /// </summary>
        public Paddle? Winner { get; private set; }

        /// <summary>
        /// Second place winner paddle that was pressed. Null is no winner
        /// </summary>
        public Paddle? Winner2 { get; private set; }

        /// <summary>
        /// Third place winner paddle that was pressed. Null is no winner
        /// </summary>
        public Paddle? Winner3 { get; private set; }

        /// <summary>
        /// Fourth place winner paddle that was pressed. Null is no winner
        /// </summary>
        public Paddle? Winner4 { get; private set; }

        /// <summary>
        /// Fifth place winner paddle that was pressed. Null is no winner
        /// </summary>
        public Paddle? Winner5 { get; private set; }

        /// <summary>
        /// Sixth place winner paddle that was pressed. Null is no winner
        /// </summary>
        public Paddle? Winner6 { get; private set; }

        /// <summary>
        /// Seventh place winner paddle that was pressed. Null is no winner
        /// </summary>
        public Paddle? Winner7 { get; private set; }

        /// <summary>
        /// Eigth place winner paddle that was pressed. Null is no winner
        /// </summary>
        public Paddle? Winner8 { get; private set; }

        /// <summary>
        /// Milliseconds from first buzz in (paddle press) for red 1 paddle. Null if no buzz in for red 1 paddle
        /// </summary>
        public decimal? Red1TimeDelta { get; private set; }

        /// <summary>
        /// Milliseconds from first buzz in (paddle press) for red 2 paddle. Null if no buzz in for red 2 paddle
        /// </summary>
        public decimal? Red2TimeDelta { get; private set; }

        /// <summary>
        /// Milliseconds from first buzz in (paddle press) for red 3 paddle. Null if no buzz in for red 3 paddle
        /// </summary>
        public decimal? Red3TimeDelta { get; private set; }

        /// <summary>
        /// Milliseconds from first buzz in (paddle press) for red 4 paddle. Null if no buzz in for red 4 paddle
        /// </summary>
        public decimal? Red4TimeDelta { get; private set; }

        /// <summary>
        /// Milliseconds from first buzz in (paddle press) for green 1 paddle. Null if no buzz in for green 1 paddle
        /// </summary>
        public decimal? Green1TimeDelta { get; private set; }

        /// <summary>
        /// Milliseconds from first buzz in (paddle press) for green 2 paddle. Null if no buzz in for green 2 paddle
        /// </summary>
        public decimal? Green2TimeDelta { get; private set; }

        /// <summary>
        /// Milliseconds from first buzz in (paddle press) for green 3 paddle. Null if no buzz in for green 3 paddle
        /// </summary>
        public decimal? Green3TimeDelta { get; private set; }

        /// <summary>
        /// Milliseconds from first buzz in (paddle press) for green 4 paddle. Null if no buzz in for green 4 paddle
        /// </summary>
        public decimal? Green4TimeDelta { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="winner1">First place winner paddle that was pressed. Null if no winner</param>
        /// <param name="winner2">Second winner paddle that was pressed. Null if no winner</param>
        /// <param name="winner3">Third winner paddle that was pressed. Null if no winner</param>
        /// <param name="winner4">Fourth winner paddle that was pressed. Null if no winner</param>
        /// <param name="winner5">Fifth winner paddle that was pressed. Null if no winner</param>
        /// <param name="winner6">Sixth winner paddle that was pressed. Null if no winner</param>
        /// <param name="winner7">Seventh winner paddle that was pressed. Null if no winner</param>
        /// <param name="winner8">Eight winner paddle that was pressed. Null if no winner</param>
        /// <param name="red1TimeDelta">Milliseconds from first buzz in (paddle press) for red 1 paddle. Null if no buzz in for red 1 paddle</param>
        /// <param name="red2TimeDelta">Milliseconds from first buzz in (paddle press) for red 2 paddle. Null if no buzz in for red 1 paddle</param>
        /// <param name="red3TimeDelta">Milliseconds from first buzz in (paddle press) for red 3 paddle. Null if no buzz in for red 1 paddle</param>
        /// <param name="red4TimeDelta">Milliseconds from first buzz in (paddle press) for red 4 paddle. Null if no buzz in for red 1 paddle</param>
        /// <param name="green1TimeDelta">Milliseconds from first buzz in (paddle press) for green 1 paddle. Null if no buzz in for red 1 paddle</param>
        /// <param name="green2TimeDelta">Milliseconds from first buzz in (paddle press) for green 2 paddle. Null if no buzz in for red 1 paddle</param>
        /// <param name="green3TimeDelta">Milliseconds from first buzz in (paddle press) for green 3 paddle. Null if no buzz in for red 1 paddle</param>
        /// <param name="green4TimeDelta">Milliseconds from first buzz in (paddle press) for green 4 paddle. Null if no buzz in for red 1 paddle</param>
        public BuzzInStatsEventArgs(Paddle? winner1, Paddle? winner2, Paddle? winner3, Paddle? winner4,
            Paddle? winner5, Paddle? winner6, Paddle? winner7, Paddle? winner8,
            decimal? red1TimeDelta, decimal? red2TimeDelta, decimal? red3TimeDelta, decimal? red4TimeDelta,
            decimal? green1TimeDelta, decimal? green2TimeDelta, decimal? green3TimeDelta, decimal? green4TimeDelta)
        {
            Winner = winner1;
            Winner2 = winner2;
            Winner3 = winner3;
            Winner4 = winner4;
            Winner5 = winner5;
            Winner6 = winner6;
            Winner7 = winner7;
            Winner8 = winner8;
            Red1TimeDelta = red1TimeDelta;
            Red2TimeDelta = red2TimeDelta;
            Red3TimeDelta = red3TimeDelta;
            Red4TimeDelta = red4TimeDelta;
            Green1TimeDelta = green1TimeDelta;
            Green2TimeDelta = green2TimeDelta;
            Green3TimeDelta = green3TimeDelta;
            Green4TimeDelta = green4TimeDelta;
        }
    }
}
