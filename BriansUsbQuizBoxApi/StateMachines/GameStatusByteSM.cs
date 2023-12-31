﻿using BriansUsbQuizBoxApi.Helpers;
using BriansUsbQuizBoxApi.Protocols;
using System;

namespace BriansUsbQuizBoxApi.StateMachines
{
    public delegate void GameStartedCallback();

    public delegate void GameLightOnCallback();

    public delegate void GameDoneCallback(Paddle? winner,
        decimal? Red1Time, decimal? Red2Time, decimal? Red3Time, decimal? Red4Time,
        decimal? Green1Time, decimal? Green2Time, decimal? Green3Time, decimal? Green4Time);

    public delegate void BuzzInStatsCallback(Paddle? winner,
        decimal? red1TimeDelta, decimal? red2TimeDelta, decimal? red3TimeDelta, decimal? red4TimeDelta,
        decimal? green1TimeDelta, decimal? green2TimeDelta, decimal? green3TimeDelta, decimal? green4TimeDelta);

    public enum QuizBoxGameState { Off, Waiting, LightOn, FirstBuzzIn, Done }

    /// <summary>
    /// Status Byte state machine for reaction time game
    /// </summary>
    public class GameStatusByteSM
    {
        private readonly GameStartedCallback _gameStartedCallback;
        private readonly GameLightOnCallback _gameLightOnCallback;
        private readonly GameDoneCallback _gameDoneCallback;
        private readonly BuzzInStatsCallback _buzzInStatsCallback;

        private QuizBoxGameState _lastGameState = QuizBoxGameState.Off;

        private bool _inGameMode = false;

        private bool _buzzInState = false;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gameLightOnCallback">Callback for the light turning on</param>
        /// <exception cref="ArgumentNullException">One or more arguments passed in where null</exception>
        public GameStatusByteSM(
            GameStartedCallback gameStartedCallback,
            GameLightOnCallback gameLightOnCallback,
            GameDoneCallback gameDoneCallback,
            BuzzInStatsCallback buzzInStatsCallback)
        {
            if (gameStartedCallback == null)
            {
                throw new ArgumentNullException(nameof(gameStartedCallback));
            }
            if (gameLightOnCallback == null)
            {
                throw new ArgumentNullException(nameof(gameLightOnCallback));
            }
            if (gameDoneCallback == null)
            {
                throw new ArgumentNullException(nameof(gameDoneCallback));
            }
            if (buzzInStatsCallback == null)
            {
                throw new ArgumentNullException(nameof(buzzInStatsCallback));
            }

            _gameStartedCallback = gameStartedCallback;
            _gameLightOnCallback = gameLightOnCallback;
            _gameDoneCallback = gameDoneCallback;
            _buzzInStatsCallback = buzzInStatsCallback;
         }

        /// <summary>
        /// Reset state machine to reset state of quiz box
        /// </summary>
        public void Reset()
        {
            _lastGameState = QuizBoxGameState.Off;
            _inGameMode = false;
        }

        /// <summary>
        /// Process a new winner byte
        /// </summary>
        /// <param name="statusByte">Status byte</param>
        public void Process(BoxStatusReport status)
        {
            var statusByte = status.Status;

            if (statusByte == StatusByte.GAME_PRESTART)
            {
                if (_lastGameState != QuizBoxGameState.Waiting)
                {
                    _gameStartedCallback();
                }

                _inGameMode = true;
                _lastGameState = QuizBoxGameState.Waiting;
            }
            else if (statusByte == StatusByte.GAME_RUNNING)
            {
                if (_lastGameState != QuizBoxGameState.LightOn)
                {
                    _gameLightOnCallback();
                }

                _lastGameState = QuizBoxGameState.LightOn;
            }
            else if (statusByte == StatusByte.GAME_DONE)
            {
                if (_inGameMode)
                {
                    if (_lastGameState != QuizBoxGameState.Done)
                    {
                        if (PaddleHelpers.TryParseWinnerByte(status.Winner, out var paddle))
                        {
                            _gameDoneCallback(paddle,
                                status.Red1Time,
                                status.Red2Time,
                                status.Red3Time,
                                status.Red4Time,
                                status.Green1Time,
                                status.Green2Time,
                                status.Green3Time,
                                status.Green4Time
                            );
                        }   
                    }

                    _lastGameState = QuizBoxGameState.Done;
                }
                else
                {
                    if (_buzzInState == false)
                    {
                        if (PaddleHelpers.TryParseWinnerByte(status.Winner, out var paddle))
                        {
                            _buzzInStatsCallback(paddle,
                                (status.Winner == WinnerByte.RED_1) ? 0 : status.Red1Time,
                                (status.Winner == WinnerByte.RED_2) ? 0 : status.Red2Time,
                                (status.Winner == WinnerByte.RED_3) ? 0 : status.Red3Time,
                                (status.Winner == WinnerByte.RED_4) ? 0 : status.Red4Time,
                                (status.Winner == WinnerByte.GREEN_1) ? 0 : status.Green1Time,
                                (status.Winner == WinnerByte.GREEN_2) ? 0 : status.Green2Time,
                                (status.Winner == WinnerByte.GREEN_3) ? 0 : status.Green3Time,
                                (status.Winner == WinnerByte.GREEN_4) ? 0 : status.Green4Time
                            );
                        }

                        _buzzInState = true;
                    }
                }
            }
            else if (statusByte == StatusByte.IDLE_MODE)
            {
                _inGameMode = false;
                _buzzInState = false;
            }
        }
    }
}
