using BriansUsbQuizBoxApi.Protocols;
using FluentAssertions;
using System;

namespace BriansUsbQuizBoxApi.Tests.Protocols
{
    public class BoxStatusTests
    {
        [Fact]
        public void StatusIdleMode()
        {
            var array = new byte[65];
            array[2] = 0x00;

            var value = BoxStatusReport.Parse(array);

            value.Status.Should().Be(StatusByte.IDLE_MODE);
            value.Winner.Should().Be(WinnerByte.NO_VALID_WINNER);
            value.Red1Time.Should().BeNull();
            value.Red2Time.Should().BeNull();
            value.Red3Time.Should().BeNull();
            value.Red4Time.Should().BeNull();
            value.Green1Time.Should().BeNull();
            value.Green2Time.Should().BeNull();
            value.Green3Time.Should().BeNull();
            value.Green4Time.Should().BeNull();
        }

        [Fact]
        public void StatusGamePrestart()
        {
            var array = new byte[65];
            array[2] = 0x01;

            var value = BoxStatusReport.Parse(array);

            value.Status.Should().Be(StatusByte.GAME_PRESTART);
            value.Winner.Should().Be(WinnerByte.NO_VALID_WINNER);
            value.Red1Time.Should().BeNull();
            value.Red2Time.Should().BeNull();
            value.Red3Time.Should().BeNull();
            value.Red4Time.Should().BeNull();
            value.Green1Time.Should().BeNull();
            value.Green2Time.Should().BeNull();
            value.Green3Time.Should().BeNull();
            value.Green4Time.Should().BeNull();
        }

        [Fact]
        public void StatusGameRunning()
        {
            var array = new byte[65];
            array[2] = 0x02;

            var value = BoxStatusReport.Parse(array);

            value.Status.Should().Be(StatusByte.GAME_RUNNING);
            value.Winner.Should().Be(WinnerByte.NO_VALID_WINNER);
            value.Red1Time.Should().BeNull();
            value.Red2Time.Should().BeNull();
            value.Red3Time.Should().BeNull();
            value.Red4Time.Should().BeNull();
            value.Green1Time.Should().BeNull();
            value.Green2Time.Should().BeNull();
            value.Green3Time.Should().BeNull();
            value.Green4Time.Should().BeNull();
        }

        [Fact]
        public void StatusGamePersonBuzzedIn()
        {
            var array = new byte[65];
            array[2] = 0x04;

            var value = BoxStatusReport.Parse(array);

            value.Status.Should().Be(StatusByte.PERSON_BUZZED_IN);
            value.Winner.Should().Be(WinnerByte.NO_VALID_WINNER);
            value.Red1Time.Should().BeNull();
            value.Red2Time.Should().BeNull();
            value.Red3Time.Should().BeNull();
            value.Red4Time.Should().BeNull();
            value.Green1Time.Should().BeNull();
            value.Green2Time.Should().BeNull();
            value.Green3Time.Should().BeNull();
            value.Green4Time.Should().BeNull();
        }

        [Fact]
        public void StatusGameDone()
        {
            var array = new byte[65];
            array[2] = 0x08;

            var value = BoxStatusReport.Parse(array);

            value.Status.Should().Be(StatusByte.GAME_DONE);
            value.Winner.Should().Be(WinnerByte.NO_VALID_WINNER);
            value.Red1Time.Should().BeNull();
            value.Red2Time.Should().BeNull();
            value.Red3Time.Should().BeNull();
            value.Red4Time.Should().BeNull();
            value.Green1Time.Should().BeNull();
            value.Green2Time.Should().BeNull();
            value.Green3Time.Should().BeNull();
            value.Green4Time.Should().BeNull();
        }

        [Fact]
        public void Status5SecondTimerRunning()
        {
            var array = new byte[65];
            array[2] = 0x10;

            var value = BoxStatusReport.Parse(array);

            value.Status.Should().Be(StatusByte.RUNNING_5_SEC_TIMER);
            value.Winner.Should().Be(WinnerByte.NO_VALID_WINNER);
            value.Red1Time.Should().BeNull();
            value.Red2Time.Should().BeNull();
            value.Red3Time.Should().BeNull();
            value.Red4Time.Should().BeNull();
            value.Green1Time.Should().BeNull();
            value.Green2Time.Should().BeNull();
            value.Green3Time.Should().BeNull();
            value.Green4Time.Should().BeNull();
        }

        [Fact]
        public void StatusExtendedTimerRunning()
        {
            var array = new byte[65];
            array[2] = 0x20;

            var value = BoxStatusReport.Parse(array);

            value.Status.Should().Be(StatusByte.EXTENDED_TIMER_RUNNING);
            value.Winner.Should().Be(WinnerByte.NO_VALID_WINNER);
            value.Red1Time.Should().BeNull();
            value.Red2Time.Should().BeNull();
            value.Red3Time.Should().BeNull();
            value.Red4Time.Should().BeNull();
            value.Green1Time.Should().BeNull();
            value.Green2Time.Should().BeNull();
            value.Green3Time.Should().BeNull();
            value.Green4Time.Should().BeNull();
        }

        [Fact]
        public void StatusStartupSequencing()
        {
            var array = new byte[65];
            array[2] = 0x40;

            var value = BoxStatusReport.Parse(array);

            value.Status.Should().Be(StatusByte.STARTUP_SEQUENCING);
            value.Winner.Should().Be(WinnerByte.NO_VALID_WINNER);
            value.Red1Time.Should().BeNull();
            value.Red2Time.Should().BeNull();
            value.Red3Time.Should().BeNull();
            value.Red4Time.Should().BeNull();
            value.Green1Time.Should().BeNull();
            value.Green2Time.Should().BeNull();
            value.Green3Time.Should().BeNull();
            value.Green4Time.Should().BeNull();
        }

        [Fact]
        public void Winner5SecondTimerExpired()
        {
            var array = new byte[65];
            array[3] = 0x01;

            var value = BoxStatusReport.Parse(array);

            value.Status.Should().Be(StatusByte.IDLE_MODE);
            value.Winner.Should().Be(WinnerByte.FIVE_SEC_TIMER_EXPIRED);
            value.Red1Time.Should().BeNull();
            value.Red2Time.Should().BeNull();
            value.Red3Time.Should().BeNull();
            value.Red4Time.Should().BeNull();
            value.Green1Time.Should().BeNull();
            value.Green2Time.Should().BeNull();
            value.Green3Time.Should().BeNull();
            value.Green4Time.Should().BeNull();
        }

        [Fact]
        public void WinnerRed4()
        {
            var array = new byte[65];
            array[3] = 0x03;

            var value = BoxStatusReport.Parse(array);

            value.Status.Should().Be(StatusByte.IDLE_MODE);
            value.Winner.Should().Be(WinnerByte.RED_4);
            value.Red1Time.Should().BeNull();
            value.Red2Time.Should().BeNull();
            value.Red3Time.Should().BeNull();
            value.Red4Time.Should().BeNull();
            value.Green1Time.Should().BeNull();
            value.Green2Time.Should().BeNull();
            value.Green3Time.Should().BeNull();
            value.Green4Time.Should().BeNull();
        }

        [Fact]
        public void WinnerRed3()
        {
            var array = new byte[65];
            array[3] = 0x04;

            var value = BoxStatusReport.Parse(array);

            value.Status.Should().Be(StatusByte.IDLE_MODE);
            value.Winner.Should().Be(WinnerByte.RED_3);
            value.Red1Time.Should().BeNull();
            value.Red2Time.Should().BeNull();
            value.Red3Time.Should().BeNull();
            value.Red4Time.Should().BeNull();
            value.Green1Time.Should().BeNull();
            value.Green2Time.Should().BeNull();
            value.Green3Time.Should().BeNull();
            value.Green4Time.Should().BeNull();
        }

        [Fact]
        public void WinnerRed2()
        {
            var array = new byte[65];
            array[3] = 0x05;

            var value = BoxStatusReport.Parse(array);

            value.Status.Should().Be(StatusByte.IDLE_MODE);
            value.Winner.Should().Be(WinnerByte.RED_2);
            value.Red1Time.Should().BeNull();
            value.Red2Time.Should().BeNull();
            value.Red3Time.Should().BeNull();
            value.Red4Time.Should().BeNull();
            value.Green1Time.Should().BeNull();
            value.Green2Time.Should().BeNull();
            value.Green3Time.Should().BeNull();
            value.Green4Time.Should().BeNull();
        }

        [Fact]
        public void WinnerRed1()
        {
            var array = new byte[65];
            array[3] = 0x06;

            var value = BoxStatusReport.Parse(array);

            value.Status.Should().Be(StatusByte.IDLE_MODE);
            value.Winner.Should().Be(WinnerByte.RED_1);
            value.Red1Time.Should().BeNull();
            value.Red2Time.Should().BeNull();
            value.Red3Time.Should().BeNull();
            value.Red4Time.Should().BeNull();
            value.Green1Time.Should().BeNull();
            value.Green2Time.Should().BeNull();
            value.Green3Time.Should().BeNull();
            value.Green4Time.Should().BeNull();
        }

        [Fact]
        public void WinnerGreen1()
        {
            var array = new byte[65];
            array[3] = 0x07;

            var value = BoxStatusReport.Parse(array);

            value.Status.Should().Be(StatusByte.IDLE_MODE);
            value.Winner.Should().Be(WinnerByte.GREEN_1);
            value.Red1Time.Should().BeNull();
            value.Red2Time.Should().BeNull();
            value.Red3Time.Should().BeNull();
            value.Red4Time.Should().BeNull();
            value.Green1Time.Should().BeNull();
            value.Green2Time.Should().BeNull();
            value.Green3Time.Should().BeNull();
            value.Green4Time.Should().BeNull();
        }

        [Fact]
        public void WinnerGreen2()
        {
            var array = new byte[65];
            array[3] = 0x08;

            var value = BoxStatusReport.Parse(array);

            value.Status.Should().Be(StatusByte.IDLE_MODE);
            value.Winner.Should().Be(WinnerByte.GREEN_2);
            value.Red1Time.Should().BeNull();
            value.Red2Time.Should().BeNull();
            value.Red3Time.Should().BeNull();
            value.Red4Time.Should().BeNull();
            value.Green1Time.Should().BeNull();
            value.Green2Time.Should().BeNull();
            value.Green3Time.Should().BeNull();
            value.Green4Time.Should().BeNull();
        }

        [Fact]
        public void WinnerGreen3()
        {
            var array = new byte[65];
            array[3] = 0x09;

            var value = BoxStatusReport.Parse(array);

            value.Status.Should().Be(StatusByte.IDLE_MODE);
            value.Winner.Should().Be(WinnerByte.GREEN_3);
            value.Red1Time.Should().BeNull();
            value.Red2Time.Should().BeNull();
            value.Red3Time.Should().BeNull();
            value.Red4Time.Should().BeNull();
            value.Green1Time.Should().BeNull();
            value.Green2Time.Should().BeNull();
            value.Green3Time.Should().BeNull();
            value.Green4Time.Should().BeNull();
        }

        [Fact]
        public void WinnerGreen4()
        {
            var array = new byte[65];
            array[3] = 0x0A;

            var value = BoxStatusReport.Parse(array);

            value.Status.Should().Be(StatusByte.IDLE_MODE);
            value.Winner.Should().Be(WinnerByte.GREEN_4);
            value.Red1Time.Should().BeNull();
            value.Red2Time.Should().BeNull();
            value.Red3Time.Should().BeNull();
            value.Red4Time.Should().BeNull();
            value.Green1Time.Should().BeNull();
            value.Green2Time.Should().BeNull();
            value.Green3Time.Should().BeNull();
            value.Green4Time.Should().BeNull();
        }

        [Fact]
        public void AllTimesNonZero()
        {
            var array = new byte[65];
            array[5] = 0x01;
            array[7] = 0x01;
            array[9] = 0x01;
            array[11] = 0x01;
            array[13] = 0x01;
            array[15] = 0x01;
            array[17] = 0x01;
            array[19] = 0x01;

            var value = BoxStatusReport.Parse(array);

            value.Status.Should().Be(StatusByte.IDLE_MODE);
            value.Winner.Should().Be(WinnerByte.NO_VALID_WINNER);
            value.Red1Time.Should().Be(998.98m);
            value.Red2Time.Should().Be(998.98m);
            value.Red3Time.Should().Be(998.98m);
            value.Red4Time.Should().Be(998.98m);
            value.Green1Time.Should().Be(998.98m);
            value.Green2Time.Should().Be(998.98m);
            value.Green3Time.Should().Be(998.98m);
            value.Green4Time.Should().Be(998.98m);
        }

        [Fact]
        public void AllNonZero()
        {
            var array = new byte[65];
            array[2] = 0x02;
            array[3] = 0x03;
            array[5] = 0x01;
            array[7] = 0x01;
            array[9] = 0x01;
            array[11] = 0x01;
            array[13] = 0x01;
            array[15] = 0x01;
            array[17] = 0x01;
            array[19] = 0x01;

            var value = BoxStatusReport.Parse(array);

            value.Status.Should().Be(StatusByte.GAME_RUNNING);
            value.Winner.Should().Be(WinnerByte.RED_4);
            value.Red1Time.Should().Be(998.98m);
            value.Red2Time.Should().Be(998.98m);
            value.Red3Time.Should().Be(998.98m);
            value.Red4Time.Should().Be(998.98m);
            value.Green1Time.Should().Be(998.98m);
            value.Green2Time.Should().Be(998.98m);
            value.Green3Time.Should().Be(998.98m);
            value.Green4Time.Should().Be(998.98m);
        }
    }
}
