using BriansUsbQuizBoxApi.Helpers;
using FluentAssertions;
using System;

namespace BriansUsbQuizBoxApi.Tests.Helpers
{
    public class TimeHelpersTests
    {
        [Fact]
        public void CalculateResponseTimeEmpty()
        {
            var ex = Record.Exception(() =>
            {
                var value = TimeHelpers.CalculateResponseTime(new byte[] { });
            });

            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public void CalculateResponseTimeSingleByte()
        {
            var ex = Record.Exception(() =>
            {
                var value = TimeHelpers.CalculateResponseTime(new byte[] { 0x00 });
            });

            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public void CalculateResponseTimeNoBuzzIn()
        {
            var value = TimeHelpers.CalculateResponseTime(new byte[] { 0x00, 0x00 });

            value.Should().BeNull();
        }

        [Fact]
        public void CalculateResponseTime()
        {
            var value = TimeHelpers.CalculateResponseTime(new byte[] { 0x00, 0x01 });

            value.Should().NotBeNull();
            value.Should().Be(998.98m);
        }

        [Fact]
        public void CalculateResponseTime325()
        {
            var value = TimeHelpers.CalculateResponseTime(new byte[] { 0x01, 0x45 });

            // As documented from the 'USB Quiz Box Interface Document' by Brian's Boxes (McKevett Enterprises) on 7/9/2013.
            // 1000 total count timer - 331.5 elapsed timer = 668.5 reaction time

            value.Should().NotBeNull();
            value.Should().Be(668.5m);
        }

        [Fact]
        public void CalculateResponseTimeAlmostMinValue()
        {
            var value = TimeHelpers.CalculateResponseTime(new byte[] { 0x03, 0xD4 });

            value.Should().NotBeNull();
            value.Should().Be(0.4m);
        }

        [Fact]
        public void CalculateResponseTimeMinValue()
        {
            // The  max observed raw value is 981 which is 1000.62 ms.  However the value is capped at 1000 ms

            var value = TimeHelpers.CalculateResponseTime(new byte[] { 0x03, 0xD5 });

            value.Should().NotBeNull();
            value.Should().Be(0m);
        }

        [Fact]
        public void IsResponseTimeZeroTrue()
        {
            var value = TimeHelpers.IsResponseTimeZero(new byte[] { 0x00, 0x00 });

            value.Should().BeTrue();
        }

        [Fact]
        public void IsResponseTimeZeroFalseOnFirstByte()
        {
            var value = TimeHelpers.IsResponseTimeZero(new byte[] { 0x01, 0x00 });

            value.Should().BeFalse();
        }

        [Fact]
        public void IsResponseTimeZeroFalseOnSecondByte()
        {
            var value = TimeHelpers.IsResponseTimeZero(new byte[] { 0x00, 0x01 });

            value.Should().BeFalse();
        }
    }
}
