﻿using BriansUsbQuizBoxApi.Protocols;
using System;
using System.Threading.Tasks;

namespace BriansUsbQuizBoxApi
{
    /// <summary>
    /// Core interface for Brian's Quiz Box Communication Protocol
    /// </summary>
    public interface IQuizBoxCoreApi : IDisposable
    {
        /// <summary>
        /// True if connected to a quiz box, otherwise false
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Type of quiz box currently connected to, null if not connected to a quiz box
        /// </summary>
        QuizBoxTypeEnum? ConnectedQuizBoxType { get; }

        /// <summary>
        /// Attempt to connect to a quiz box
        /// </summary>
        /// <returns>True if connection successful, otherwise false</returns>
        bool Connect();

        /// <summary>
        /// Write a command to the quiz box
        /// </summary>
        /// <param name="command"></param>
        void WriteCommand(BoxCommandReport command);

        /// <summary>
        /// Synchronous quiz box status read
        /// </summary>
        /// <returns>Quiz box status, otherwise null</returns>
        BoxStatusReport? ReadStatus();

        /// <summary>
        /// Asynchronous quiz box status read
        /// </summary>
        /// <returns>Quiz box status, otherwise null</returns>
        Task<BoxStatusReport?> ReadStatusAsync();

        /// <summary>
        /// Disconnect from a connected quiz box
        /// </summary>
        void Disconnect();
    }
}
