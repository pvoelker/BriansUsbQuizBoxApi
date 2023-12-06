using BriansUsbQuizBoxApi.Exceptions;
using BriansUsbQuizBoxApi.Protocols;
using FluentAssertions;
using Moq;
using System;

namespace BriansUsbQuizBoxApi.Tests
{
    /// <summary>
    /// Quiz Box API Tests
    /// 
    /// NOTE: If you are seeing problems with tests not running, make sure to check the
    ///       'test output'.  Exceptions thrown into the 'I/O thread' sometimes get
    ///       swallowed up...
    /// </summary>
    public class QuizBoxApiTests
    {
        [Fact]
        public void SimpleConnection()
        {
            var isConnected = false;
            var coreApiMock = new Mock<IQuizBoxCoreApi>(MockBehavior.Strict);
            coreApiMock.SetupGet(m => m.IsConnected).Returns(() => { return isConnected; });
            coreApiMock.Setup(m => m.Connect()).Returns(() =>
            {
                isConnected = true;
                return true;
            });
            coreApiMock.Setup(m => m.Disconnect());
            coreApiMock.Setup(m => m.Dispose());
            coreApiMock.Setup(m => m.WriteCommand(It.IsAny<BoxCommandReport>()));
            coreApiMock.Setup(m => m.ReadStatus()).Returns(new BoxStatusReport(StatusByte.IDLE_MODE, WinnerByte.NO_VALID_WINNER,
                0, 0, 0, 0, 0, 0, 0 ,0));

            using (var api = new QuizBoxApi(coreApiMock.Object))
            {
                api.BuzzIn += (s, e) => { Assert.Fail("This should not be called"); };
                api.FiveSecondTimerStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.FiveSecondTimerExpired += (s, e) => { Assert.Fail("This should not be called"); };
                api.LockoutTimerStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.LockoutTimerExpired += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameLightOn += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameDone += (s, e) => { Assert.Fail("This should not be called"); };
                api.DisconnectionDetected += (s, e) => { Assert.Fail("This should not be called"); };

                api.IsConnected.Should().BeFalse();

                api.Connect();

                api.IsConnected.Should().BeTrue();

                Thread.Sleep(40); // Allow read thread to run a bit
            }
        }

        [Fact]
        public void BuzzIn()
        {
            var isConnected = false;
            var statusQueue = new Queue<BoxStatusReport>();
            var coreApiMock = new Mock<IQuizBoxCoreApi>(MockBehavior.Strict);
            coreApiMock.SetupGet(m => m.IsConnected).Returns(() => { return isConnected; });
            coreApiMock.Setup(m => m.Connect()).Returns(() =>
            {
                isConnected = true;
                return true;
            });
            coreApiMock.Setup(m => m.Disconnect());
            coreApiMock.Setup(m => m.Dispose());
            coreApiMock.Setup(m => m.WriteCommand(It.IsAny<BoxCommandReport>()));
            coreApiMock.Setup(m => m.ReadStatus()).Returns(() => DequeueStatus(statusQueue));

            var eventFiredCount = 0;

            using (var api = new QuizBoxApi(coreApiMock.Object))
            {
                api.BuzzIn += (s, e) => { eventFiredCount++; };
                api.FiveSecondTimerStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.FiveSecondTimerExpired += (s, e) => { Assert.Fail("This should not be called"); };
                api.LockoutTimerStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.LockoutTimerExpired += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameLightOn += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameDone += (s, e) => { Assert.Fail("This should not be called"); };
                api.DisconnectionDetected += (s, e) => { Assert.Fail("This should not be called"); };

                api.IsConnected.Should().BeFalse();

                api.Connect();

                api.IsConnected.Should().BeTrue();

                Thread.Sleep(20);

                statusQueue.Enqueue(new BoxStatusReport(StatusByte.IDLE_MODE, WinnerByte.RED_1, 0, 0, 0, 0, 0, 0, 0, 0));

                Thread.Sleep(20); // Allow read thread to run a bit
            }

            eventFiredCount.Should().Be(1);
        }

        [Fact]
        public void FiveSecondTimerStart()
        {
            var isConnected = false;
            var statusQueue = new Queue<BoxStatusReport>();
            var coreApiMock = new Mock<IQuizBoxCoreApi>(MockBehavior.Strict);
            coreApiMock.SetupGet(m => m.IsConnected).Returns(() => { return isConnected; });
            coreApiMock.Setup(m => m.Connect()).Returns(() =>
            {
                isConnected = true;
                return true;
            });
            coreApiMock.Setup(m => m.Disconnect());
            coreApiMock.Setup(m => m.Dispose());
            coreApiMock.Setup(m => m.WriteCommand(It.IsAny<BoxCommandReport>()));
            coreApiMock.Setup(m => m.ReadStatus()).Returns(() => DequeueStatus(statusQueue));

            var eventFiredCount = 0;

            using (var api = new QuizBoxApi(coreApiMock.Object))
            {
                api.BuzzIn += (s, e) => { Assert.Fail("This should not be called"); };
                api.FiveSecondTimerStarted += (s, e) => { eventFiredCount++; };
                api.FiveSecondTimerExpired += (s, e) => { Assert.Fail("This should not be called"); };
                api.LockoutTimerStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.LockoutTimerExpired += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameLightOn += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameDone += (s, e) => { Assert.Fail("This should not be called"); };
                api.DisconnectionDetected += (s, e) => { Assert.Fail("This should not be called"); };

                api.IsConnected.Should().BeFalse();

                api.Connect();

                api.IsConnected.Should().BeTrue();

                Thread.Sleep(20);

                statusQueue.Enqueue(new BoxStatusReport(StatusByte.RUNNING_5_SEC_TIMER, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));

                Thread.Sleep(20); // Allow read thread to run a bit
            }

            eventFiredCount.Should().Be(1);
        }

        [Fact]
        public void FiveSecondTimerExpired()
        {
            var isConnected = false;
            var statusQueue = new Queue<BoxStatusReport>();
            var coreApiMock = new Mock<IQuizBoxCoreApi>(MockBehavior.Strict);
            coreApiMock.SetupGet(m => m.IsConnected).Returns(() => { return isConnected; });
            coreApiMock.Setup(m => m.Connect()).Returns(() =>
            {
                isConnected = true;
                return true;
            });
            coreApiMock.Setup(m => m.Disconnect());
            coreApiMock.Setup(m => m.Dispose());
            coreApiMock.Setup(m => m.WriteCommand(It.IsAny<BoxCommandReport>()));
            coreApiMock.Setup(m => m.ReadStatus()).Returns(() => DequeueStatus(statusQueue));

            var eventFiredCount = 0;

            using (var api = new QuizBoxApi(coreApiMock.Object))
            {
                api.BuzzIn += (s, e) => { Assert.Fail("This should not be called"); };
                api.FiveSecondTimerStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.FiveSecondTimerExpired += (s, e) => { eventFiredCount++; };
                api.LockoutTimerStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.LockoutTimerExpired += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameLightOn += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameDone += (s, e) => { Assert.Fail("This should not be called"); };
                api.DisconnectionDetected += (s, e) => { Assert.Fail("This should not be called"); };

                api.IsConnected.Should().BeFalse();

                api.Connect();

                api.IsConnected.Should().BeTrue();

                Thread.Sleep(20);

                statusQueue.Enqueue(new BoxStatusReport(StatusByte.IDLE_MODE, WinnerByte.FIVE_SEC_TIMER_EXPIRED, 0, 0, 0, 0, 0, 0, 0, 0));

                Thread.Sleep(20); // Allow read thread to run a bit
            }

            eventFiredCount.Should().Be(1);
        }

        [Fact]
        public void PaddleLockoutTimerStart()
        {
            var isConnected = false;
            var statusQueue = new Queue<BoxStatusReport>();
            var coreApiMock = new Mock<IQuizBoxCoreApi>(MockBehavior.Strict);
            coreApiMock.SetupGet(m => m.IsConnected).Returns(() => { return isConnected; });
            coreApiMock.Setup(m => m.Connect()).Returns(() =>
            {
                isConnected = true;
                return true;
            });
            coreApiMock.Setup(m => m.Disconnect());
            coreApiMock.Setup(m => m.Dispose());
            coreApiMock.Setup(m => m.WriteCommand(It.IsAny<BoxCommandReport>()));
            coreApiMock.Setup(m => m.ReadStatus()).Returns(() => DequeueStatus(statusQueue));

            var eventFiredCount = 0;

            using (var api = new QuizBoxApi(coreApiMock.Object))
            {
                api.BuzzIn += (s, e) => { Assert.Fail("This should not be called"); };
                api.FiveSecondTimerStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.FiveSecondTimerExpired += (s, e) => { Assert.Fail("This should not be called"); };
                api.LockoutTimerStarted += (s, e) => { eventFiredCount++; };
                api.LockoutTimerExpired += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameLightOn += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameDone += (s, e) => { Assert.Fail("This should not be called"); };
                api.DisconnectionDetected += (s, e) => { Assert.Fail("This should not be called"); };

                api.IsConnected.Should().BeFalse();

                api.Connect();

                api.IsConnected.Should().BeTrue();

                Thread.Sleep(20);

                // Queue up a bunch of these so we never hit the 'lockout expired' state
                statusQueue.Enqueue(new BoxStatusReport(StatusByte.EXTENDED_TIMER_RUNNING, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
                statusQueue.Enqueue(new BoxStatusReport(StatusByte.EXTENDED_TIMER_RUNNING, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
                statusQueue.Enqueue(new BoxStatusReport(StatusByte.EXTENDED_TIMER_RUNNING, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
                statusQueue.Enqueue(new BoxStatusReport(StatusByte.EXTENDED_TIMER_RUNNING, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
                statusQueue.Enqueue(new BoxStatusReport(StatusByte.EXTENDED_TIMER_RUNNING, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
                statusQueue.Enqueue(new BoxStatusReport(StatusByte.EXTENDED_TIMER_RUNNING, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
                statusQueue.Enqueue(new BoxStatusReport(StatusByte.EXTENDED_TIMER_RUNNING, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
                statusQueue.Enqueue(new BoxStatusReport(StatusByte.EXTENDED_TIMER_RUNNING, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));

                Thread.Sleep(20); // Allow read thread to run a bit
            }

            eventFiredCount.Should().Be(1);
        }

        [Fact]
        public void PaddleLockoutTimerExpired()
        {
            var isConnected = false;
            var statusQueue = new Queue<BoxStatusReport>();
            var coreApiMock = new Mock<IQuizBoxCoreApi>(MockBehavior.Strict);
            coreApiMock.SetupGet(m => m.IsConnected).Returns(() => { return isConnected; });
            coreApiMock.Setup(m => m.Connect()).Returns(() =>
            {
                isConnected = true;
                return true;
            });
            coreApiMock.Setup(m => m.Disconnect());
            coreApiMock.Setup(m => m.Dispose());
            coreApiMock.Setup(m => m.WriteCommand(It.IsAny<BoxCommandReport>()));
            coreApiMock.Setup(m => m.ReadStatus()).Returns(() => DequeueStatus(statusQueue));

            var eventFiredCount = 0;

            using (var api = new QuizBoxApi(coreApiMock.Object))
            {
                api.BuzzIn += (s, e) => { Assert.Fail("This should not be called"); };
                api.FiveSecondTimerStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.FiveSecondTimerExpired += (s, e) => { Assert.Fail("This should not be called"); };
                api.LockoutTimerStarted += (s, e) => { /* Don't care */ };
                api.LockoutTimerExpired += (s, e) => { eventFiredCount++; };
                api.GameStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameLightOn += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameDone += (s, e) => { Assert.Fail("This should not be called"); };
                api.DisconnectionDetected += (s, e) => { Assert.Fail("This should not be called"); };

                api.IsConnected.Should().BeFalse();

                api.Connect();

                api.IsConnected.Should().BeTrue();

                Thread.Sleep(20);

                statusQueue.Enqueue(new BoxStatusReport(StatusByte.EXTENDED_TIMER_RUNNING, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
                statusQueue.Enqueue(new BoxStatusReport(StatusByte.IDLE_MODE, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
                statusQueue.Enqueue(new BoxStatusReport(StatusByte.IDLE_MODE, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
                statusQueue.Enqueue(new BoxStatusReport(StatusByte.IDLE_MODE, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
                statusQueue.Enqueue(new BoxStatusReport(StatusByte.IDLE_MODE, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
                statusQueue.Enqueue(new BoxStatusReport(StatusByte.IDLE_MODE, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
                statusQueue.Enqueue(new BoxStatusReport(StatusByte.IDLE_MODE, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
                statusQueue.Enqueue(new BoxStatusReport(StatusByte.IDLE_MODE, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));

                Thread.Sleep(20); // Allow read thread to run a bit
            }

            eventFiredCount.Should().Be(1);
        }

        [Fact]
        public void GameStarted()
        {
            var isConnected = false;
            var statusQueue = new Queue<BoxStatusReport>();
            var coreApiMock = new Mock<IQuizBoxCoreApi>(MockBehavior.Strict);
            coreApiMock.SetupGet(m => m.IsConnected).Returns(() => { return isConnected; });
            coreApiMock.Setup(m => m.Connect()).Returns(() =>
            {
                isConnected = true;
                return true;
            });
            coreApiMock.Setup(m => m.Disconnect());
            coreApiMock.Setup(m => m.Dispose());
            coreApiMock.Setup(m => m.WriteCommand(It.IsAny<BoxCommandReport>()));
            coreApiMock.Setup(m => m.ReadStatus()).Returns(() => DequeueStatus(statusQueue));

            var eventFiredCount = 0;

            using (var api = new QuizBoxApi(coreApiMock.Object))
            {
                api.BuzzIn += (s, e) => { Assert.Fail("This should not be called"); };
                api.FiveSecondTimerStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.FiveSecondTimerExpired += (s, e) => { Assert.Fail("This should not be called"); };
                api.LockoutTimerStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.LockoutTimerExpired += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameStarted += (s, e) => { eventFiredCount++; };
                api.GameLightOn += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameDone += (s, e) => { Assert.Fail("This should not be called"); };
                api.DisconnectionDetected += (s, e) => { Assert.Fail("This should not be called"); };

                api.IsConnected.Should().BeFalse();

                api.Connect();

                api.IsConnected.Should().BeTrue();

                Thread.Sleep(20);

                statusQueue.Enqueue(new BoxStatusReport(StatusByte.GAME_PRESTART, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));

                Thread.Sleep(20); // Allow read thread to run a bit
            }

            eventFiredCount.Should().Be(1);
        }

        [Fact]
        public void GameYellowLightOn()
        {
            var isConnected = false;
            var statusQueue = new Queue<BoxStatusReport>();
            var coreApiMock = new Mock<IQuizBoxCoreApi>(MockBehavior.Strict);
            coreApiMock.SetupGet(m => m.IsConnected).Returns(() => { return isConnected; });
            coreApiMock.Setup(m => m.Connect()).Returns(() =>
            {
                isConnected = true;
                return true;
            });
            coreApiMock.Setup(m => m.Disconnect());
            coreApiMock.Setup(m => m.Dispose());
            coreApiMock.Setup(m => m.WriteCommand(It.IsAny<BoxCommandReport>()));
            coreApiMock.Setup(m => m.ReadStatus()).Returns(() => DequeueStatus(statusQueue));

            var eventFiredCount = 0;

            using (var api = new QuizBoxApi(coreApiMock.Object))
            {
                api.BuzzIn += (s, e) => { Assert.Fail("This should not be called"); };
                api.FiveSecondTimerStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.FiveSecondTimerExpired += (s, e) => { Assert.Fail("This should not be called"); };
                api.LockoutTimerStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.LockoutTimerExpired += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameLightOn += (s, e) => { eventFiredCount++; };
                api.GameDone += (s, e) => { Assert.Fail("This should not be called"); };
                api.DisconnectionDetected += (s, e) => { Assert.Fail("This should not be called"); };

                api.IsConnected.Should().BeFalse();

                api.Connect();

                api.IsConnected.Should().BeTrue();

                Thread.Sleep(20);

                statusQueue.Enqueue(new BoxStatusReport(StatusByte.GAME_RUNNING, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));

                Thread.Sleep(20); // Allow read thread to run a bit
            }

            eventFiredCount.Should().Be(1);
        }

        [Fact]
        public void GameDone()
        {
            var isConnected = false;
            var statusQueue = new Queue<BoxStatusReport>();
            var coreApiMock = new Mock<IQuizBoxCoreApi>(MockBehavior.Strict);
            coreApiMock.SetupGet(m => m.IsConnected).Returns(() => { return isConnected; });
            coreApiMock.Setup(m => m.Connect()).Returns(() =>
            {
                isConnected = true;
                return true;
            });
            coreApiMock.Setup(m => m.Disconnect());
            coreApiMock.Setup(m => m.Disconnect());
            coreApiMock.Setup(m => m.Dispose());
            coreApiMock.Setup(m => m.WriteCommand(It.IsAny<BoxCommandReport>()));
            coreApiMock.Setup(m => m.ReadStatus()).Returns(() => DequeueStatus(statusQueue));

            var eventFiredCount = 0;

            using (var api = new QuizBoxApi(coreApiMock.Object))
            {
                api.BuzzIn += (s, e) => { Assert.Fail("This should not be called"); };
                api.FiveSecondTimerStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.FiveSecondTimerExpired += (s, e) => { Assert.Fail("This should not be called"); };
                api.LockoutTimerStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.LockoutTimerExpired += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameStarted += (s, e) => { /* Don't care */ };
                api.GameLightOn += (s, e) => { /* Don't care */ };
                api.GameDone += (s, e) => { eventFiredCount++; };
                api.DisconnectionDetected += (s, e) => { Assert.Fail("This should not be called"); };

                api.IsConnected.Should().BeFalse();

                api.Connect();

                api.IsConnected.Should().BeTrue();

                Thread.Sleep(20);

                statusQueue.Enqueue(new BoxStatusReport(StatusByte.GAME_PRESTART, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
                statusQueue.Enqueue(new BoxStatusReport(StatusByte.GAME_RUNNING, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
                statusQueue.Enqueue(new BoxStatusReport(StatusByte.GAME_DONE, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));

                Thread.Sleep(50); // Allow read thread to run a bit
            }

            eventFiredCount.Should().Be(1);
        }

        [Fact]
        public void DisconnectionDetected()
        {
            var statusRet = new BoxStatusReport(StatusByte.IDLE_MODE, WinnerByte.NO_VALID_WINNER,
                0, 0, 0, 0, 0, 0, 0, 0);

            var isConnected = false;
            var coreApiMock = new Mock<IQuizBoxCoreApi>(MockBehavior.Strict);
            coreApiMock.SetupGet(m => m.IsConnected).Returns(() => { return isConnected; });
            coreApiMock.Setup(m => m.Connect()).Returns(() =>
            {
                isConnected = true;
                return true;
            });
            coreApiMock.Setup(m => m.Disconnect());
            coreApiMock.Setup(m => m.Dispose());
            coreApiMock.Setup(m => m.WriteCommand(It.IsAny<BoxCommandReport>()));
            coreApiMock.SetupSequence(m => m.ReadStatus())
                .Returns(statusRet)
                .Returns(statusRet)
                .Returns(statusRet)
                .Throws(new DisconnectionException("boom"));

            var eventFiredCount = 0;

            using (var api = new QuizBoxApi(coreApiMock.Object))
            {
                api.BuzzIn += (s, e) => { Assert.Fail("This should not be called"); };
                api.FiveSecondTimerStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.FiveSecondTimerExpired += (s, e) => { Assert.Fail("This should not be called"); };
                api.LockoutTimerStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.LockoutTimerExpired += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameLightOn += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameDone += (s, e) => { Assert.Fail("This should not be called"); };
                api.DisconnectionDetected += (s, e) => { eventFiredCount++; };

                api.IsConnected.Should().BeFalse();

                api.Connect();

                api.IsConnected.Should().BeTrue();

                Thread.Sleep(100); // Allow read thread to run long enough to hit the exception throw
            }

            eventFiredCount.Should().Be(1);
        }

        static BoxStatusReport DequeueStatus(Queue<BoxStatusReport> queue)
        {
            if (queue.TryDequeue(out var command))
            {
                return command;
            }
            else
            {
                return new BoxStatusReport(StatusByte.IDLE_MODE, WinnerByte.NO_VALID_WINNER,
                    0, 0, 0, 0, 0, 0, 0, 0);
            }
        }
    }
}
