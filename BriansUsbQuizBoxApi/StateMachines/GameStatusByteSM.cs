using BriansUsbQuizBoxApi.Protocols;
using System;

namespace BriansUsbQuizBoxApi.StateMachines
{
    public delegate void GameStartedCallback();

    public delegate void GameLightOnCallback();

    public delegate void GameFirstBuzzInCallback();

    public delegate void GameDoneCallback(decimal Red1Time, decimal Red2Time, decimal Red3Time, decimal Red4Time,
        decimal Green1Time, decimal Green2Time, decimal Green3Time, decimal Green4Time);

    public enum QuizBoxGameState { Off, Waiting, LightOn, FirstBuzzIn, Done }

    /// <summary>
    /// Status Byte state machine
    /// </summary>
    public class GameStatusByteSM
    {
        private GameStartedCallback _gameStartedCallback;
        private GameLightOnCallback _gameLightOnCallback;
        private GameFirstBuzzInCallback _gameFirstBuzzInCallback;
        private GameDoneCallback _gameDoneCallback;

        private QuizBoxGameState _lastGameState = QuizBoxGameState.Off;

        private bool _inGameMode = false;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gameLightOnCallback">Callback for the light turning on</param>
        /// <exception cref="ArgumentNullException">One or more arguments passed in where null</exception>
        public GameStatusByteSM(
            GameStartedCallback gameStartedCallback,
            GameLightOnCallback gameLightOnCallback,
            GameFirstBuzzInCallback gameFirstBuzzInCallback,
            GameDoneCallback gameDoneCallback)
        {
            if (gameStartedCallback == null)
            {
                throw new ArgumentNullException(nameof(gameStartedCallback));
            }
            if (gameLightOnCallback == null)
            {
                throw new ArgumentNullException(nameof(gameLightOnCallback));
            }
            if (gameFirstBuzzInCallback == null)
            {
                throw new ArgumentNullException(nameof(gameFirstBuzzInCallback));
            }
            if (gameDoneCallback == null)
            {
                throw new ArgumentNullException(nameof(gameDoneCallback));
            }

            _gameStartedCallback = gameStartedCallback;
            _gameLightOnCallback = gameLightOnCallback;
            _gameFirstBuzzInCallback = gameFirstBuzzInCallback;
            _gameDoneCallback = gameDoneCallback;
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
        public void Process(BoxStatus status)
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
            else if (statusByte == StatusByte.PERSON_BUZZED_IN)
            {
                if (_inGameMode)
                {
                    if (_lastGameState != QuizBoxGameState.FirstBuzzIn)
                    {
                        _gameFirstBuzzInCallback();
                    }

                    _lastGameState = QuizBoxGameState.FirstBuzzIn;
                }
            }
            else if (statusByte == StatusByte.GAME_DONE)
            {
                if (_lastGameState != QuizBoxGameState.Done)
                {
                    _gameDoneCallback(status.Red1Time, status.Red2Time, status.Red3Time, status.Red4Time,
                        status.Green1Time, status.Green2Time, status.Green3Time, status.Green4Time);
                }

                _lastGameState = QuizBoxGameState.Done;
            }
            else if (statusByte == StatusByte.IDLE_MODE)
            {
                _inGameMode = false;
            }
        }
    }
}
