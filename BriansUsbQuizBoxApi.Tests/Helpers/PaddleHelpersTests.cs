using BriansUsbQuizBoxApi.Helpers;
using FluentAssertions;
using System;

namespace BriansUsbQuizBoxApi.Tests.Helpers
{
    public class PaddleHelpersTests
    {
        [Fact]
        public void PaddleRed1()
        {
            PaddleHelpers.TryParseWinnerByte(BriansUsbQuizBoxApi.Protocols.WinnerByte.RED_1, out var paddle);

            paddle.Should().Be(Paddle.RED_1);
        }

        [Fact]
        public void PaddleRed2()
        {
            PaddleHelpers.TryParseWinnerByte(BriansUsbQuizBoxApi.Protocols.WinnerByte.RED_2, out var paddle);

            paddle.Should().Be(Paddle.RED_2);
        }

        [Fact]
        public void PaddleRed3()
        {
            PaddleHelpers.TryParseWinnerByte(BriansUsbQuizBoxApi.Protocols.WinnerByte.RED_3, out var paddle);

            paddle.Should().Be(Paddle.RED_3);
        }

        [Fact]
        public void PaddleRed4()
        {
            PaddleHelpers.TryParseWinnerByte(BriansUsbQuizBoxApi.Protocols.WinnerByte.RED_4, out var paddle);

            paddle.Should().Be(Paddle.RED_4);
        }

        [Fact]
        public void PaddleGreen1()
        {
            PaddleHelpers.TryParseWinnerByte(BriansUsbQuizBoxApi.Protocols.WinnerByte.GREEN_1, out var paddle);

            paddle.Should().Be(Paddle.GREEN_1);
        }

        [Fact]
        public void PaddleGreen2()
        {
            PaddleHelpers.TryParseWinnerByte(BriansUsbQuizBoxApi.Protocols.WinnerByte.GREEN_2, out var paddle);

            paddle.Should().Be(Paddle.GREEN_2);
        }

        [Fact]
        public void PaddleGreen3()
        {
            PaddleHelpers.TryParseWinnerByte(BriansUsbQuizBoxApi.Protocols.WinnerByte.GREEN_3, out var paddle);

            paddle.Should().Be(Paddle.GREEN_3);
        }

        [Fact]
        public void PaddleGreen4()
        {
            PaddleHelpers.TryParseWinnerByte(BriansUsbQuizBoxApi.Protocols.WinnerByte.GREEN_4, out var paddle);

            paddle.Should().Be(Paddle.GREEN_4);
        }

        [Fact]
        public void FiveSecondTimeout()
        {
            PaddleHelpers.TryParseWinnerByte(BriansUsbQuizBoxApi.Protocols.WinnerByte.FIVE_SEC_TIMER_EXPIRED, out var paddle);

            paddle.Should().BeNull();
        }

        [Fact]
        public void NoValidWinner()
        {
            PaddleHelpers.TryParseWinnerByte(BriansUsbQuizBoxApi.Protocols.WinnerByte.NO_VALID_WINNER, out var paddle);

            paddle.Should().BeNull();
        }
    }
}
