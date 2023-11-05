using BriansUsbQuizBoxApi.Protocols;
using BriansUsbQuizBoxApi.StateMachines;
using FluentAssertions;
using System;

namespace BriansUsbQuizBoxApi.Tests.StateMachines
{
    public class StatusByteSMTests
    {
        [Fact]
        public void QuizBoxReady()
        {
            var callbackCalled = false;

            var sm = new StatusByteSM(() => callbackCalled = true,
                () => Assert.Fail("This should not be called"),
                () => Assert.Fail("This should not be called"),
                () => Assert.Fail("This should not be called"));

            sm.Process(StatusByte.IDLE_MODE);

            callbackCalled.Should().BeTrue();
        }

        [Fact]
        public void MultipleIdleModes()
        {
            var callbackCount = 0;

            var sm = new StatusByteSM(() => callbackCount++,
                () => Assert.Fail("This should not be called"),
                () => Assert.Fail("This should not be called"),
                () => Assert.Fail("This should not be called"));

            sm.Process(StatusByte.IDLE_MODE);
            sm.Process(StatusByte.IDLE_MODE);
            sm.Process(StatusByte.IDLE_MODE);

            callbackCount.Should().Be(1);
        }

        [Fact]
        public void FiveSecondTimerStarted()
        {
            var callbackCalled = false;

            var sm = new StatusByteSM(() => Assert.Fail("This should not be called"),
                () => callbackCalled = true,
                () => Assert.Fail("This should not be called"),
                () => Assert.Fail("This should not be called"));

            sm.Process(StatusByte.RUNNING_5_SEC_TIMER);

            callbackCalled.Should().BeTrue();
        }

        [Fact]
        public void ExtendedTimerRunning()
        {
            var callbackCalled = false;

            var sm = new StatusByteSM(() => Assert.Fail("This should not be called"),
                () => Assert.Fail("This should not be called"),
                () => callbackCalled = true,
                () => Assert.Fail("This should not be called"));

            sm.Process(StatusByte.EXTENDED_TIMER_RUNNING);

            callbackCalled.Should().BeTrue();
        }

        [Fact]
        public void ExtendedTimerExpired()
        {
            var callbackCalled = false;

            var sm = new StatusByteSM(() => { /* Don't care if this is called */ },
                () => Assert.Fail("This should not be called"),
                () => { /* Don't care if this is called */ },
                () => callbackCalled = true);

            sm.Process(StatusByte.EXTENDED_TIMER_RUNNING);
            sm.Process(StatusByte.IDLE_MODE);

            callbackCalled.Should().BeTrue();
        }
    }
}
