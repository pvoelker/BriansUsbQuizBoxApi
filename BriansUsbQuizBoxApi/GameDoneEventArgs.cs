﻿using System;

namespace BriansUsbQuizBoxApi
{
    /// <summary>
    /// Event arguments for when a reaction game completes.  Maximum reaction time is 1000ms
    /// </summary>
    public class GameDoneEventArgs : EventArgs
    {
        /// <summary>
        /// Winner paddle color that was pressed. 'None' if no winner
        /// </summary>
        public PaddleColorEnum WinnerPaddleColor { get; private set; }

        /// <summary>
        /// Winner paddle number that was pressed. 'None' if no winner
        /// </summary>
        public PaddleNumberEnum WinnerPaddleNumber { get; private set; }

        /// <summary>
        /// Reaction time for red 1 paddle in milliseconds
        /// </summary>
        public decimal Red1Time { get; private set; }

        /// <summary>
        /// Reaction time for red 2 paddle in milliseconds
        /// </summary>
        public decimal Red2Time { get; private set; }

        /// <summary>
        /// Reaction time for red 3 paddle in milliseconds
        /// </summary>
        public decimal Red3Time { get; private set; }

        /// <summary>
        /// Reaction time for red 4 paddle in milliseconds
        /// </summary>
        public decimal Red4Time { get; private set; }

        /// <summary>
        /// Reaction time for green 1 paddle in milliseconds
        /// </summary>
        public decimal Green1Time { get; private set; }

        /// <summary>
        /// Reaction time for green 2 paddle in milliseconds
        /// </summary>
        public decimal Green2Time { get; private set; }

        /// <summary>
        /// Reaction time for green 3 paddle in milliseconds
        /// </summary>
        public decimal Green3Time { get; private set; }

        /// <summary>
        /// Reaction time for green 4 paddle in milliseconds
        /// </summary>
        public decimal Green4Time { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <para name="winnerPaddleColor">Winner paddle color that was pressed. 'None' if no winner</para>
        /// <para name="winnerPaddleNumber">Winner paddle number that was pressed. 'None' if no winner</para>
        /// <param name="red1Time">Reaction time for red 1 paddle in milliseconds</param>
        /// <param name="red2Time">Reaction time for red 2 paddle in milliseconds</param>
        /// <param name="red3Time">Reaction time for red 3 paddle in milliseconds</param>
        /// <param name="red4Time">Reaction time for red 4 paddle in milliseconds</param>
        /// <param name="green1Time">Reaction time for green 1 paddle in milliseconds</param>
        /// <param name="green2Time">Reaction time for green 2 paddle in milliseconds</param>
        /// <param name="green3Time">Reaction time for green 3 paddle in milliseconds</param>
        /// <param name="green4Time">Reaction time for green 4 paddle in milliseconds</param>
        public GameDoneEventArgs(PaddleColorEnum winnerPaddleColor, PaddleNumberEnum winnerPaddleNumber,
            decimal red1Time, decimal red2Time, decimal red3Time, decimal red4Time,
            decimal green1Time, decimal green2Time, decimal green3Time, decimal green4Time)
        {
            WinnerPaddleColor = winnerPaddleColor;
            WinnerPaddleNumber = winnerPaddleNumber;
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
