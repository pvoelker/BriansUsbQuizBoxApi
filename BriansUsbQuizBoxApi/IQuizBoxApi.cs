using BriansUsbQuizBoxApi.Exceptions;
using BriansUsbQuizBoxApi.Protocols;
using BriansUsbQuizBoxApi.StateMachines;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BriansUsbQuizBoxApi
{
    public interface IQuizBoxApi : IDisposable
    {
        #region Events

        /// <summary>
        /// Event fired when quiz box is ready
        /// </summary>
        event EventHandler? QuizBoxReady;

        /// <summary>
        /// Event fired when someone buzzes in
        /// </summary>
        event EventHandler<BuzzInEventArgs>? BuzzIn;

        /// <summary>
        /// Event fired when the five second timer is started
        /// </summary>
        /// <remarks>This event is not meant for exact timings</remarks>
        event EventHandler? FiveSecondTimerStarted;

        /// <summary>
        /// Event fired when the five second timer is expired. Paddles are locked out
        /// </summary>
        /// <remarks>This event is not meant for exact timings</remarks>
        event EventHandler? FiveSecondTimerExpired;

        /// <summary>
        /// Event fired when a paddle lockout timer is started
        /// </summary>
        /// <remarks>This event is not meant for exact timings</remarks>
        event EventHandler? LockoutTimerStarted;

        /// <summary>
        /// Event fired when a paddle lockout timer is expired
        /// </summary>
        /// <remarks>This event is not meant for exact timings</remarks>
        event EventHandler? LockoutTimerExpired;

        /// <summary>
        /// Event fired when a reaction time game is started. Players need to watch for the yellow light to come on the quiz box
        /// </summary>
        /// <remarks>This event is not meant for exact timings</remarks>
        event EventHandler? GameStarted;

        /// <summary>
        /// Event fired when the yellow quiz box light comes on.  Players need to press their paddles as quickly as possible
        /// </summary>
        /// <remarks>This event is not meant for exact timings</remarks>
        event EventHandler? GameLightOn;

        /// <summary>
        /// Event fired when the first player of the reaction time game buzzes in
        /// </summary>
        /// <remarks>This event is not meant for exact timings</remarks>
        event EventHandler? GameFirstBuzzIn;

        /// <summary>
        /// Event fired when the reaction time game has completed.  Results are included in the event arguments
        /// </summary>
        /// <remarks>This event is not meant for exact timings</remarks>
        event EventHandler<GameDoneEventArgs>? GameDone;

        /// <summary>
        /// Event fired when a disconnection occurs in the background read thread
        /// </summary>
        event EventHandler<DisconnectionEventArgs>? ReadThreadDisconnection;

        #endregion

        /// <summary>
        /// True if connected to a quiz box, otherwise false
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Attempt to connect to a quiz box
        /// </summary>
        /// <returns>True if connection successful, otherwise false</returns>
        bool Connect();

        /// <summary>
        /// Send the reset command to the quiz box.  All timers and game modes are cancelled
        /// </summary>
        void Reset();

        /// <summary>
        /// Send the command to start the five second timer to the quiz box.  Paddles will be locked out after the timer expires
        /// </summary>
        void Start5SecondBuzzInTimer();

        /// <summary>
        /// Send the command to start the paddle lockout timer
        /// </summary>
        /// <param name="timer">The length of the lockout timer</param>
        void StartPaddleLockoutTimer(LockoutTimerEnum timer);

        /// <summary>
        /// Lockout quiz box paddles (indefinite paddle lockout timer)
        /// </summary>
        void StartPaddleLockout();

        /// <summary>
        /// Stop the lockout on quiz box paddles
        /// </summary>
        void StopPaddleLockout();

        /// <summary>
        /// Attempts to start the reaction time game.  The 'GameStarted' event will fire if the game was started
        /// </summary>
        void StartReactionTimingGame();

        /// <summary>
        /// Disconnect from USB quiz box
        /// </summary>
        void Disconnect();
    }
}
