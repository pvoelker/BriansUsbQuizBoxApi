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
            PaddleHelpers.TryParseWinnerByte(BriansUsbQuizBoxApi.Protocols.WinnerByte.RED_1, out var paddleNumber, out var paddleColor);

            paddleNumber.Should().Be(PaddleNumberEnum.Paddle1);
            paddleColor.Should().Be(PaddleColorEnum.Red);
        }

        [Fact]
        public void PaddleRed2()
        {
            PaddleHelpers.TryParseWinnerByte(BriansUsbQuizBoxApi.Protocols.WinnerByte.RED_2, out var paddleNumber, out var paddleColor);

            paddleNumber.Should().Be(PaddleNumberEnum.Paddle2);
            paddleColor.Should().Be(PaddleColorEnum.Red);
        }

        [Fact]
        public void PaddleRed3()
        {
            PaddleHelpers.TryParseWinnerByte(BriansUsbQuizBoxApi.Protocols.WinnerByte.RED_3, out var paddleNumber, out var paddleColor);

            paddleNumber.Should().Be(PaddleNumberEnum.Paddle3);
            paddleColor.Should().Be(PaddleColorEnum.Red);
        }

        [Fact]
        public void PaddleRed4()
        {
            PaddleHelpers.TryParseWinnerByte(BriansUsbQuizBoxApi.Protocols.WinnerByte.RED_4, out var paddleNumber, out var paddleColor);

            paddleNumber.Should().Be(PaddleNumberEnum.Paddle4);
            paddleColor.Should().Be(PaddleColorEnum.Red);
        }

        [Fact]
        public void PaddleGreen1()
        {
            PaddleHelpers.TryParseWinnerByte(BriansUsbQuizBoxApi.Protocols.WinnerByte.GREEN_1, out var paddleNumber, out var paddleColor);

            paddleNumber.Should().Be(PaddleNumberEnum.Paddle1);
            paddleColor.Should().Be(PaddleColorEnum.Green);
        }

        [Fact]
        public void PaddleGreen2()
        {
            PaddleHelpers.TryParseWinnerByte(BriansUsbQuizBoxApi.Protocols.WinnerByte.GREEN_2, out var paddleNumber, out var paddleColor);

            paddleNumber.Should().Be(PaddleNumberEnum.Paddle2);
            paddleColor.Should().Be(PaddleColorEnum.Green);
        }

        [Fact]
        public void PaddleGreen3()
        {
            PaddleHelpers.TryParseWinnerByte(BriansUsbQuizBoxApi.Protocols.WinnerByte.GREEN_3, out var paddleNumber, out var paddleColor);

            paddleNumber.Should().Be(PaddleNumberEnum.Paddle3);
            paddleColor.Should().Be(PaddleColorEnum.Green);
        }

        [Fact]
        public void PaddleGreen4()
        {
            PaddleHelpers.TryParseWinnerByte(BriansUsbQuizBoxApi.Protocols.WinnerByte.GREEN_4, out var paddleNumber, out var paddleColor);

            paddleNumber.Should().Be(PaddleNumberEnum.Paddle4);
            paddleColor.Should().Be(PaddleColorEnum.Green);
        }

        [Fact]
        public void FiveSecondTimeout()
        {
            PaddleHelpers.TryParseWinnerByte(BriansUsbQuizBoxApi.Protocols.WinnerByte.FIVE_SEC_TIMER_EXPIRED, out var paddleNumber, out var paddleColor);

            paddleNumber.Should().Be(PaddleNumberEnum.None);
            paddleColor.Should().Be(PaddleColorEnum.None);
        }

        [Fact]
        public void NoValidWinner()
        {
            PaddleHelpers.TryParseWinnerByte(BriansUsbQuizBoxApi.Protocols.WinnerByte.NO_VALID_WINNER, out var paddleNumber, out var paddleColor);

            paddleNumber.Should().Be(PaddleNumberEnum.None);
            paddleColor.Should().Be(PaddleColorEnum.None);
        }
    }
}
