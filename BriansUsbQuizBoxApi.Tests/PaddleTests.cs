using FluentAssertions;
using System;

namespace BriansUsbQuizBoxApi.Tests
{
    public class PaddleTests
    {
        [Fact]
        public void EqualOperator()
        {
#pragma warning disable CS1718 // Comparison made to same variable
            (Paddle.RED_1 == Paddle.RED_1).Should().BeTrue();
#pragma warning restore CS1718 // Comparison made to same variable

            (Paddle.RED_1 == Paddle.RED_2).Should().BeFalse();

            (Paddle.RED_1 == Paddle.GREEN_1).Should().BeFalse();
        }

        [Fact]
        public void EqualsMethod()
        {
            (Paddle.RED_1.Equals(Paddle.RED_1)).Should().BeTrue();

            (Paddle.RED_1.Equals(Paddle.RED_2)).Should().BeFalse();

            (Paddle.RED_1.Equals(Paddle.GREEN_1)).Should().BeFalse();
        }

        [Fact]
        public void NotEqualOperator()
        {
#pragma warning disable CS1718 // Comparison made to same variable
            (Paddle.RED_1 != Paddle.RED_1).Should().BeFalse();
#pragma warning restore CS1718 // Comparison made to same variable

            (Paddle.RED_1 != Paddle.RED_2).Should().BeTrue();

            (Paddle.RED_1 != Paddle.GREEN_1).Should().BeTrue();
        }

        [Fact]
        public void HashCode()
        {
            (Paddle.RED_1.GetHashCode() == Paddle.RED_1.GetHashCode()).Should().BeTrue();

            (Paddle.RED_1.GetHashCode() == Paddle.RED_2.GetHashCode()).Should().BeFalse();

            (Paddle.RED_1.GetHashCode() == Paddle.GREEN_1.GetHashCode()).Should().BeFalse();
        }
    }
}
