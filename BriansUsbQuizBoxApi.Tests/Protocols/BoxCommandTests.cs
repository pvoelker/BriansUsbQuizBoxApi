using BriansUsbQuizBoxApi.Protocols;
using FluentAssertions;
using System;

namespace BriansUsbQuizBoxApi.Tests.Protocols
{
    public class BoxCommandTests
    {
        [Fact]
        public void StatusRequest()
        {
            var value = new BoxCommandReport { CommandHeader = CommandHeaderByte.STATUS_REQUEST };

            var array = value.BuildByteArray();

            array.Length.Should().Be(65);

            array[0].Should().Be(0x00);

            array[1].Should().Be(0x86);

            array.Skip(2).ToArray().Should().OnlyContain((x) => x == 0x00);
        }

        [Fact]
        public void Start30SecondTimer()
        {
            var value = new BoxCommandReport { CommandHeader = CommandHeaderByte.START_30_SEC_TIMER };

            var array = value.BuildByteArray();

            array.Length.Should().Be(65);

            array[0].Should().Be(0x00);

            array[1].Should().Be(0x88);

            array.Skip(2).ToArray().Should().OnlyContain((x) => x == 0x00);
        }

        [Fact]
        public void Zero()
        {
            var value = new BoxCommandReport { CommandHeader = 0 };

            var ex = Record.Exception(() =>
            {
                var array = value.BuildByteArray();
            });      

            ex.Should().BeOfType<ArgumentOutOfRangeException>();
        }
    }
}
