﻿using BriansUsbQuizBoxApi.Helpers;
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
        public void CalculateResponseTimeZero()
        {
            var value = TimeHelpers.CalculateResponseTime(new byte[] { 0x00, 0x00 });

            value.Should().Be(0);
        }

        [Fact]
        public void CalculateResponseTime1()
        {
            var value = TimeHelpers.CalculateResponseTime(new byte[] { 0x01, 0x00 });

            value.Should().Be(1.02m);
        }

        [Fact]
        public void CalculateResponseTimeMaxValue()
        {
            var value = TimeHelpers.CalculateResponseTime(new byte[] { 0xFF, 0xFF });

            value.Should().Be(66845.7m);
        }
    }
}
