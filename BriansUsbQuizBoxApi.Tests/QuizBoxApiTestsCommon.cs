using BriansUsbQuizBoxApi.Exceptions;
using BriansUsbQuizBoxApi.Protocols;
using FluentAssertions;
using Moq;
using System;

namespace BriansUsbQuizBoxApi.Tests
{
    /// <summary>
    /// Quiz Box API Tests for all protocol versions
    /// 
    /// NOTE: If you are seeing problems with tests not running, make sure to check the
    ///       'test output'.  Exceptions thrown into the 'I/O thread' sometimes get
    ///       swallowed up...
    /// </summary>
    public class QuizBoxApiTestsCommon
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

            bool connectionCompleteFired = false;
            using (var api = new QuizBoxApi(coreApiMock.Object))
            {
                api.ConnectionComplete += (s, e) => { connectionCompleteFired = true; };
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
                connectionCompleteFired.Should().BeFalse();

                api.Connect();

                api.IsConnected.Should().BeTrue();
                connectionCompleteFired.Should().BeFalse();

                Thread.Sleep(40); // Allow read thread to run a bit

                connectionCompleteFired.Should().BeTrue();
            }
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
    }
}
