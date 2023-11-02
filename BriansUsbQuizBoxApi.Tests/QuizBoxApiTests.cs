using BriansUsbQuizBoxApi.Protocols;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BriansUsbQuizBoxApi.Tests
{
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
            coreApiMock.Setup(m => m.Dispose());
            coreApiMock.Setup(m => m.WriteCommand(It.IsAny<BoxCommandReport>()));
            coreApiMock.Setup(m => m.ReadStatus()).Returns(new BoxStatusReport(StatusByte.IDLE_MODE, WinnerByte.NO_VALID_WINNER,
                0, 0, 0, 0, 0, 0, 0 ,0));

            var eventFiredCount = 0;

            using (var api = new QuizBoxApi(coreApiMock.Object))
            {
                api.QuizBoxReady += (s, e) => { eventFiredCount++; };
                api.BuzzIn += (s, e) => { Assert.Fail("This should not be called"); };
                api.FiveSecondTimerStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.FiveSecondTimerExpired += (s, e) => { Assert.Fail("This should not be called"); };
                api.LockoutTimerStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.LockoutTimerExpired += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameLightOn += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameFirstBuzzIn += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameDone += (s, e) => { Assert.Fail("This should not be called"); };
                api.ReadThreadDisconnection += (s, e) => { Assert.Fail("This should not be called"); };

                api.IsConnected.Should().BeFalse();

                api.Connect();

                api.IsConnected.Should().BeTrue();

                Thread.Sleep(40); // Allow read thread to run a bit
            }

            eventFiredCount.Should().Be(1);
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
            coreApiMock.Setup(m => m.Dispose());
            coreApiMock.Setup(m => m.WriteCommand(It.IsAny<BoxCommandReport>()));
            coreApiMock.Setup(m => m.ReadStatus()).Returns(() =>
            {
                if (statusQueue.TryDequeue(out var command))
                {
                    return command;
                }
                else
                {
                    return new BoxStatusReport(StatusByte.IDLE_MODE, WinnerByte.NO_VALID_WINNER,
                        0, 0, 0, 0, 0, 0, 0, 0);
                }
            });

            var eventFiredCount = 0;

            using (var api = new QuizBoxApi(coreApiMock.Object))
            {
                api.QuizBoxReady += (s, e) => { /* Don't care */ };
                api.BuzzIn += (s, e) => { eventFiredCount++; };
                api.FiveSecondTimerStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.FiveSecondTimerExpired += (s, e) => { Assert.Fail("This should not be called"); };
                api.LockoutTimerStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.LockoutTimerExpired += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameStarted += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameLightOn += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameFirstBuzzIn += (s, e) => { Assert.Fail("This should not be called"); };
                api.GameDone += (s, e) => { Assert.Fail("This should not be called"); };
                api.ReadThreadDisconnection += (s, e) => { Assert.Fail("This should not be called"); };

                api.IsConnected.Should().BeFalse();

                api.Connect();

                api.IsConnected.Should().BeTrue();

                Thread.Sleep(20);

                // Simulate buzz in
                statusQueue.Enqueue(new BoxStatusReport(StatusByte.IDLE_MODE, WinnerByte.RED_1, 0, 0, 0, 0, 0, 0, 0, 0));

                Thread.Sleep(20); // Allow read thread to run a bit
            }

            eventFiredCount.Should().Be(1);
        }
    }
}
