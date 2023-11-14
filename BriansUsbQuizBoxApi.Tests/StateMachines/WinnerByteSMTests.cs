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
            Paddle? paddle = null;

            var sm = new WinnerByteSM((p) => { paddle = p; },
                () => Assert.Fail("This should not be called"));

            sm.Process(StatusByte.PERSON_BUZZED_IN, WinnerByte.RED_1);

            paddle.Should().Be(Paddle.RED_1);
        }

        [Fact]
        public void PaddleRed2Pressed()
        {
            Paddle? paddle = null;

            var sm = new WinnerByteSM((p) => { paddle = p; },
                () => Assert.Fail("This should not be called"));

            sm.Process(StatusByte.PERSON_BUZZED_IN, WinnerByte.RED_2);

            paddle.Should().Be(Paddle.RED_2);
        }

        [Fact]
        public void PaddleRed3Pressed()
        {
            Paddle? paddle = null;

            var sm = new WinnerByteSM((p) => { paddle = p; },
                () => Assert.Fail("This should not be called"));

            sm.Process(StatusByte.PERSON_BUZZED_IN, WinnerByte.RED_3);

            paddle.Should().Be(Paddle.RED_3);
        }

        [Fact]
        public void PaddleRed4Pressed()
        {
            Paddle? paddle = null;

            var sm = new WinnerByteSM((p) => { paddle = p; },
                () => Assert.Fail("This should not be called"));

            sm.Process(StatusByte.PERSON_BUZZED_IN, WinnerByte.RED_4);

            paddle.Should().Be(Paddle.RED_4);
        }

        [Fact]
        public void PaddleGreen1Pressed()
        {
            Paddle? paddle = null;

            var sm = new WinnerByteSM((p) => { paddle = p; },
                () => Assert.Fail("This should not be called"));

            sm.Process(StatusByte.PERSON_BUZZED_IN, WinnerByte.GREEN_1);

            paddle.Should().Be(Paddle.GREEN_1);
        }

        [Fact]
        public void PaddleGreen2Pressed()
        {
            Paddle? paddle = null;

            var sm = new WinnerByteSM((p) => { paddle = p; },
                () => Assert.Fail("This should not be called"));

            sm.Process(StatusByte.PERSON_BUZZED_IN, WinnerByte.GREEN_2);

            paddle.Should().Be(Paddle.GREEN_2);
        }

        [Fact]
        public void PaddleGreen3Pressed()
        {
            Paddle? paddle = null;

            var sm = new WinnerByteSM((p) => { paddle = p; },
                () => Assert.Fail("This should not be called"));

            sm.Process(StatusByte.PERSON_BUZZED_IN, WinnerByte.GREEN_3);

            paddle.Should().Be(Paddle.GREEN_3);
        }

        [Fact]
        public void PaddleGreen4Pressed()
        {
            Paddle? paddle = null;

            var sm = new WinnerByteSM((p) => { paddle = p; },
                () => Assert.Fail("This should not be called"));

            sm.Process(StatusByte.PERSON_BUZZED_IN, WinnerByte.GREEN_4);

            paddle.Should().Be(Paddle.GREEN_4);
        }

        [Fact]
        public void FiveSecondTimerExpired()
        {
            bool timerExpired = false;

            var sm = new WinnerByteSM((p) => Assert.Fail("This should not be called"),
                () => { timerExpired = true; });

            sm.Process(StatusByte.PERSON_BUZZED_IN, WinnerByte.FIVE_SEC_TIMER_EXPIRED);

            timerExpired.Should().BeTrue();
        }

        [Fact]
        public void NoValidWinner()
        {
            var sm = new WinnerByteSM((p) => Assert.Fail("This should not be called"),
                () => Assert.Fail("This should not be called"));

            sm.Process(StatusByte.PERSON_BUZZED_IN, WinnerByte.NO_VALID_WINNER);

            // No callbacks should be called
        }
    }
}
