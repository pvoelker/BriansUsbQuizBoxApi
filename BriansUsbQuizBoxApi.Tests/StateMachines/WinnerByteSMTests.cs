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
            PaddleNumberEnum number = PaddleNumberEnum.None;

            var sm = new WinnerByteSM((x, y) => { color = x; number = y; }, () => Assert.Fail("This should not be called"));

            sm.Process(StatusByte.PERSON_BUZZED_IN, WinnerByte.RED_1);

            color.Should().Be(PaddleColorEnum.Red);
            number.Should().Be(PaddleNumberEnum.Paddle1);
        }

        [Fact]
        public void PaddleRed2Pressed()
        {
            PaddleColorEnum color = PaddleColorEnum.None;
            PaddleNumberEnum number = PaddleNumberEnum.None;

            var sm = new WinnerByteSM((x, y) => { color = x; number = y; }, () => Assert.Fail("This should not be called"));

            sm.Process(StatusByte.PERSON_BUZZED_IN, WinnerByte.RED_2);

            color.Should().Be(PaddleColorEnum.Red);
            number.Should().Be(PaddleNumberEnum.Paddle2);
        }

        [Fact]
        public void PaddleRed3Pressed()
        {
            PaddleColorEnum color = PaddleColorEnum.None;
            PaddleNumberEnum number = PaddleNumberEnum.None;

            var sm = new WinnerByteSM((x, y) => { color = x; number = y; }, () => Assert.Fail("This should not be called"));

            sm.Process(StatusByte.PERSON_BUZZED_IN, WinnerByte.RED_3);

            color.Should().Be(PaddleColorEnum.Red);
            number.Should().Be(PaddleNumberEnum.Paddle3);
        }

        [Fact]
        public void PaddleRed4Pressed()
        {
            PaddleColorEnum color = PaddleColorEnum.None;
            PaddleNumberEnum number = PaddleNumberEnum.None;

            var sm = new WinnerByteSM((x, y) => { color = x; number = y; }, () => Assert.Fail("This should not be called"));

            sm.Process(StatusByte.PERSON_BUZZED_IN, WinnerByte.RED_4);

            color.Should().Be(PaddleColorEnum.Red);
            number.Should().Be(PaddleNumberEnum.Paddle4);
        }

        [Fact]
        public void PaddleGreen1Pressed()
        {
            PaddleColorEnum color = PaddleColorEnum.None;
            PaddleNumberEnum number = PaddleNumberEnum.None;

            var sm = new WinnerByteSM((x, y) => { color = x; number = y; }, () => Assert.Fail("This should not be called"));

            sm.Process(StatusByte.PERSON_BUZZED_IN, WinnerByte.GREEN_1);

            color.Should().Be(PaddleColorEnum.Green);
            number.Should().Be(PaddleNumberEnum.Paddle1);
        }

        [Fact]
        public void PaddleGreen2Pressed()
        {
            PaddleColorEnum color = PaddleColorEnum.None;
            PaddleNumberEnum number = PaddleNumberEnum.None;

            var sm = new WinnerByteSM((x, y) => { color = x; number = y; }, () => Assert.Fail("This should not be called"));

            sm.Process(StatusByte.PERSON_BUZZED_IN, WinnerByte.GREEN_2);

            color.Should().Be(PaddleColorEnum.Green);
            number.Should().Be(PaddleNumberEnum.Paddle2);
        }

        [Fact]
        public void PaddleGreen3Pressed()
        {
            PaddleColorEnum color = PaddleColorEnum.None;
            PaddleNumberEnum number = PaddleNumberEnum.None;

            var sm = new WinnerByteSM((x, y) => { color = x; number = y; }, () => Assert.Fail("This should not be called"));

            sm.Process(StatusByte.PERSON_BUZZED_IN, WinnerByte.GREEN_3);

            color.Should().Be(PaddleColorEnum.Green);
            number.Should().Be(PaddleNumberEnum.Paddle3);
        }

        [Fact]
        public void PaddleGreen4Pressed()
        {
            PaddleColorEnum color = PaddleColorEnum.None;
            PaddleNumberEnum number = PaddleNumberEnum.None;

            var sm = new WinnerByteSM((x, y) => { color = x; number = y; }, () => Assert.Fail("This should not be called"));

            sm.Process(StatusByte.PERSON_BUZZED_IN, WinnerByte.GREEN_4);

            color.Should().Be(PaddleColorEnum.Green);
            number.Should().Be(PaddleNumberEnum.Paddle4);
        }

        [Fact]
        public void FiveSecondTimerExpired()
        {
            bool timerExpired = false;

            var sm = new WinnerByteSM((x, y) => Assert.Fail("This should not be called"), () => { timerExpired = true; });

            sm.Process(StatusByte.PERSON_BUZZED_IN, WinnerByte.FIVE_SEC_TIMER_EXPIRED);

            timerExpired.Should().BeTrue();
        }

        [Fact]
        public void NoValidWinner()
        {
            var sm = new WinnerByteSM((x, y) => Assert.Fail("This should not be called"),
                () => Assert.Fail("This should not be called"));

            sm.Process(StatusByte.PERSON_BUZZED_IN, WinnerByte.NO_VALID_WINNER);

            // No callbacks should be called
        }
    }
}
