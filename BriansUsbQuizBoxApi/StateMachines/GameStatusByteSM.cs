using BriansUsbQuizBoxApi.Helpers;
using BriansUsbQuizBoxApi.Protocols;
using System;

namespace BriansUsbQuizBoxApi.StateMachines
{
    internal delegate void GameStartedCallback();

    internal delegate void GameLightOnCallback();

    internal delegate void GameDoneCallback(Paddle? winner1, Paddle? winner2, Paddle? winner3, Paddle? winner4,
        Paddle? winner5, Paddle? winner6, Paddle? winner7, Paddle? winner8,
        decimal? Red1Time, decimal? Red2Time, decimal? Red3Time, decimal? Red4Time,
        decimal? Green1Time, decimal? Green2Time, decimal? Green3Time, decimal? Green4Time);

    internal delegate void BuzzInStatsCallback(Paddle? winner1, Paddle? winner2, Paddle? winner3, Paddle? winner4,
        Paddle? winner5, Paddle? winner6, Paddle? winner7, Paddle? winner8,
        decimal? red1TimeDelta, decimal? red2TimeDelta, decimal? red3TimeDelta, decimal? red4TimeDelta,
        decimal? green1TimeDelta, decimal? green2TimeDelta, decimal? green3TimeDelta, decimal? green4TimeDelta);

    public enum QuizBoxGameState { Off, Waiting, LightOn, FirstBuzzIn, Done }

    /// <summary>
    /// Status Byte state machine for reaction time game
    /// </summary>
    internal class GameStatusByteSM
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
            _gameStartedCallback = gameStartedCallback ?? throw new ArgumentNullException(nameof(gameStartedCallback));
            _gameLightOnCallback = gameLightOnCallback ?? throw new ArgumentNullException(nameof(gameLightOnCallback));
            _gameDoneCallback = gameDoneCallback ?? throw new ArgumentNullException(nameof(gameDoneCallback));
            _buzzInStatsCallback = buzzInStatsCallback ?? throw new ArgumentNullException(nameof(buzzInStatsCallback));
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
                        if (PaddleHelpers.TryParseWinnerByte(status.Winner, out var paddle1)
                            && PaddleHelpers.TryParseWinnerByte(status.Winner2, out var paddle2)
                            && PaddleHelpers.TryParseWinnerByte(status.Winner3, out var paddle3)
                            && PaddleHelpers.TryParseWinnerByte(status.Winner4, out var paddle4)
                            && PaddleHelpers.TryParseWinnerByte(status.Winner5, out var paddle5)
                            && PaddleHelpers.TryParseWinnerByte(status.Winner6, out var paddle6)
                            && PaddleHelpers.TryParseWinnerByte(status.Winner7, out var paddle7)
                            && PaddleHelpers.TryParseWinnerByte(status.Winner8, out var paddle8))
                        {
                            _gameDoneCallback(paddle1, paddle2, paddle3, paddle4,
                                paddle5, paddle6, paddle7, paddle8,
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
                        if (PaddleHelpers.TryParseWinnerByte(status.Winner, out var paddle1)
                            && PaddleHelpers.TryParseWinnerByte(status.Winner2, out var paddle2)
                            && PaddleHelpers.TryParseWinnerByte(status.Winner3, out var paddle3)
                            && PaddleHelpers.TryParseWinnerByte(status.Winner4, out var paddle4)
                            && PaddleHelpers.TryParseWinnerByte(status.Winner5, out var paddle5)
                            && PaddleHelpers.TryParseWinnerByte(status.Winner6, out var paddle6)
                            && PaddleHelpers.TryParseWinnerByte(status.Winner7, out var paddle7)
                            && PaddleHelpers.TryParseWinnerByte(status.Winner8, out var paddle8))
                        {
                            _buzzInStatsCallback(paddle1, paddle2, paddle3, paddle4,
                                paddle5, paddle6, paddle7, paddle8,
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
