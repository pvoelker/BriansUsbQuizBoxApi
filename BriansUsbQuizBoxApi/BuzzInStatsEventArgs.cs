﻿using System;

namespace BriansUsbQuizBoxApi
{
    /// <summary>
    /// Event arguments for when buzz in statistics are sent by the quiz box.  Typically after a 5 second timer or one second after an initial buzz in (paddle press)
    /// </summary>
    public class BuzzInStatsEventArgs : EventArgs
    {
        /// <summary>
        /// Winner paddle that was pressed. Null is no winner
        /// </summary>
        public Paddle? Winner { get; private set; }

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
        /// <param name="winner">Winner paddle that was pressed. Null if no winner</param>
        /// <param name="red1TimeDelta">Milliseconds from first buzz in (paddle press) for red 1 paddle. Null if no buzz in for red 1 paddle</param>
        /// <param name="red2TimeDelta">Milliseconds from first buzz in (paddle press) for red 2 paddle. Null if no buzz in for red 1 paddle</param>
        /// <param name="red3TimeDelta">Milliseconds from first buzz in (paddle press) for red 3 paddle. Null if no buzz in for red 1 paddle</param>
        /// <param name="red4TimeDelta">Milliseconds from first buzz in (paddle press) for red 4 paddle. Null if no buzz in for red 1 paddle</param>
        /// <param name="green1TimeDelta">Milliseconds from first buzz in (paddle press) for green 1 paddle. Null if no buzz in for red 1 paddle</param>
        /// <param name="green2TimeDelta">Milliseconds from first buzz in (paddle press) for green 2 paddle. Null if no buzz in for red 1 paddle</param>
        /// <param name="green3TimeDelta">Milliseconds from first buzz in (paddle press) for green 3 paddle. Null if no buzz in for red 1 paddle</param>
        /// <param name="green4TimeDelta">Milliseconds from first buzz in (paddle press) for green 4 paddle. Null if no buzz in for red 1 paddle</param>
        public BuzzInStatsEventArgs(Paddle? winner,
            decimal? red1TimeDelta, decimal? red2TimeDelta, decimal? red3TimeDelta, decimal? red4TimeDelta,
            decimal? green1TimeDelta, decimal? green2TimeDelta, decimal? green3TimeDelta, decimal? green4TimeDelta)
        {
            Winner = winner;
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
