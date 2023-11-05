using BriansUsbQuizBoxApi.Protocols;
using BriansUsbQuizBoxApi.StateMachines;
using FluentAssertions;
using System;

namespace BriansUsbQuizBoxApi.Tests.StateMachines
{
    public class WinnerByteSMTests
    {
        [Fact]
        public void PaddleRed1Pressed()
        {
            PaddleColorEnum color = PaddleColorEnum.None;
            int number = -1;

            var sm = new WinnerByteSM((x, y) => { color = x; number = y; }, () => Assert.Fail("This should not be called"));

            sm.Process(WinnerByte.RED_1);

            color.Should().Be(PaddleColorEnum.Red);
            number.Should().Be(1);
        }

        [Fact]
        public void PaddleRed2Pressed()
        {
            PaddleColorEnum color = PaddleColorEnum.None;
            int number = -1;

            var sm = new WinnerByteSM((x, y) => { color = x; number = y; }, () => Assert.Fail("This should not be called"));

            sm.Process(WinnerByte.RED_2);

            color.Should().Be(PaddleColorEnum.Red);
            number.Should().Be(2);
        }

        [Fact]
        public void PaddleRed3Pressed()
        {
            PaddleColorEnum color = PaddleColorEnum.None;
            int number = -1;

            var sm = new WinnerByteSM((x, y) => { color = x; number = y; }, () => Assert.Fail("This should not be called"));

            sm.Process(WinnerByte.RED_3);

            color.Should().Be(PaddleColorEnum.Red);
            number.Should().Be(3);
        }

        [Fact]
        public void PaddleRed4Pressed()
        {
            PaddleColorEnum color = PaddleColorEnum.None;
            int number = -1;

            var sm = new WinnerByteSM((x, y) => { color = x; number = y; }, () => Assert.Fail("This should not be called"));

            sm.Process(WinnerByte.RED_4);

            color.Should().Be(PaddleColorEnum.Red);
            number.Should().Be(4);
        }

        [Fact]
        public void PaddleGreen1Pressed()
        {
            PaddleColorEnum color = PaddleColorEnum.None;
            int number = -1;

            var sm = new WinnerByteSM((x, y) => { color = x; number = y; }, () => Assert.Fail("This should not be called"));

            sm.Process(WinnerByte.GREEN_1);

            color.Should().Be(PaddleColorEnum.Green);
            number.Should().Be(1);
        }

        [Fact]
        public void PaddleGreen2Pressed()
        {
            PaddleColorEnum color = PaddleColorEnum.None;
            int number = -1;

            var sm = new WinnerByteSM((x, y) => { color = x; number = y; }, () => Assert.Fail("This should not be called"));

            sm.Process(WinnerByte.GREEN_2);

            color.Should().Be(PaddleColorEnum.Green);
            number.Should().Be(2);
        }

        [Fact]
        public void PaddleGreen3Pressed()
        {
            PaddleColorEnum color = PaddleColorEnum.None;
            int number = -1;

            var sm = new WinnerByteSM((x, y) => { color = x; number = y; }, () => Assert.Fail("This should not be called"));

            sm.Process(WinnerByte.GREEN_3);

            color.Should().Be(PaddleColorEnum.Green);
            number.Should().Be(3);
        }

        [Fact]
        public void PaddleGreen4Pressed()
        {
            PaddleColorEnum color = PaddleColorEnum.None;
            int number = -1;

            var sm = new WinnerByteSM((x, y) => { color = x; number = y; }, () => Assert.Fail("This should not be called"));

            sm.Process(WinnerByte.GREEN_4);

            color.Should().Be(PaddleColorEnum.Green);
            number.Should().Be(4);
        }

        [Fact]
        public void FiveSecondTimerExpired()
        {
            bool timerExpired = false;

            var sm = new WinnerByteSM((x, y) => Assert.Fail("This should not be called"), () => { timerExpired = true; });

            sm.Process(WinnerByte.FIVE_SEC_TIMER_EXPIRED);

            timerExpired.Should().BeTrue();
        }

        [Fact]
        public void NoValidWinner()
        {
            var sm = new WinnerByteSM((x, y) => Assert.Fail("This should not be called"),
                () => Assert.Fail("This should not be called"));

            sm.Process(WinnerByte.NO_VALID_WINNER);

            // No callbacks should be called
        }
    }
}
