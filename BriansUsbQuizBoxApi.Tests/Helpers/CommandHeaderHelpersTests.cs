using BriansUsbQuizBoxApi.Helpers;
using BriansUsbQuizBoxApi.Protocols;
using FluentAssertions;
using System;

namespace BriansUsbQuizBoxApi.Tests.Helpers
{
    public class CommandHeaderHelpersTests
    {
        [Fact]
        public void IsStatusReturned_StartReactionTimingGame()
        {
            CommandHeaderByte.START_REACTION_TIMING_GAME.IsStatusReturned().Should().BeTrue();
        }

        [Fact]
        public void IsStatusReturned_Start5SecondTimer()
        {
            CommandHeaderByte.START_5_SEC_TIMER.IsStatusReturned().Should().BeTrue();
        }

        [Fact]
        public void IsStatusReturned_StatusRequest()
        {
            CommandHeaderByte.STATUS_REQUEST.IsStatusReturned().Should().BeTrue();
        }

        [Fact]
        public void IsStatusReturned_Clear()
        {
            CommandHeaderByte.CLEAR.IsStatusReturned().Should().BeFalse();
        }

        [Fact]
        public void IsStatusReturned_Start30SecondTimer()
        {
            CommandHeaderByte.START_30_SEC_TIMER.IsStatusReturned().Should().BeFalse();
        }

        [Fact]
        public void IsStatusReturned_Start1MinuteTimer()
        {
            CommandHeaderByte.START_1_MIN_TIMER.IsStatusReturned().Should().BeFalse();
        }

        [Fact]
        public void IsStatusReturned_Start2MinuteTimer()
        {
            CommandHeaderByte.START_2_MIN_TIMER.IsStatusReturned().Should().BeFalse();
        }

        [Fact]
        public void IsStatusReturned_Start3MinuteTimer()
        {
            CommandHeaderByte.START_3_MIN_TIMER.IsStatusReturned().Should().BeFalse();
        }

        [Fact]
        public void IsStatusReturned_StartInfiniteTimer()
        {
            CommandHeaderByte.START_INFINITE_TIMER.IsStatusReturned().Should().BeFalse();
        }

        [Fact]
        public void IsStatusReturned_EndInfiniteTimer()
        {
            CommandHeaderByte.END_INFINITE_TIMER_BUZZ.IsStatusReturned().Should().BeFalse();
        }

        [Fact]
        public void GetExpectedStatusLogic_Clear()
        {
            CommandHeaderByte.CLEAR.GetExpectedStatusLogic()(StatusByte.IDLE_MODE).Should().BeTrue();

            CommandHeaderByte.CLEAR.GetExpectedStatusLogic()(StatusByte.GAME_PRESTART).Should().BeFalse();

            CommandHeaderByte.CLEAR.GetExpectedStatusLogic()(StatusByte.GAME_RUNNING).Should().BeFalse();

            CommandHeaderByte.CLEAR.GetExpectedStatusLogic()(StatusByte.PERSON_BUZZED_IN).Should().BeFalse();

            CommandHeaderByte.CLEAR.GetExpectedStatusLogic()(StatusByte.GAME_DONE).Should().BeFalse();

            CommandHeaderByte.CLEAR.GetExpectedStatusLogic()(StatusByte.RUNNING_5_SEC_TIMER).Should().BeFalse();

            CommandHeaderByte.CLEAR.GetExpectedStatusLogic()(StatusByte.EXTENDED_TIMER_RUNNING).Should().BeFalse();

            CommandHeaderByte.CLEAR.GetExpectedStatusLogic()(StatusByte.STARTUP_SEQUENCING).Should().BeFalse();
        }

        [Fact]
        public void GetExpectedStatusLogic_StartReactionTimingGame()
        {
            CommandHeaderByte.START_REACTION_TIMING_GAME.GetExpectedStatusLogic()(StatusByte.IDLE_MODE).Should().BeFalse();

            CommandHeaderByte.START_REACTION_TIMING_GAME.GetExpectedStatusLogic()(StatusByte.GAME_PRESTART).Should().BeTrue();

            CommandHeaderByte.START_REACTION_TIMING_GAME.GetExpectedStatusLogic()(StatusByte.GAME_RUNNING).Should().BeFalse();

            CommandHeaderByte.START_REACTION_TIMING_GAME.GetExpectedStatusLogic()(StatusByte.PERSON_BUZZED_IN).Should().BeFalse();

            CommandHeaderByte.START_REACTION_TIMING_GAME.GetExpectedStatusLogic()(StatusByte.GAME_DONE).Should().BeFalse();

            CommandHeaderByte.START_REACTION_TIMING_GAME.GetExpectedStatusLogic()(StatusByte.RUNNING_5_SEC_TIMER).Should().BeFalse();

            CommandHeaderByte.START_REACTION_TIMING_GAME.GetExpectedStatusLogic()(StatusByte.EXTENDED_TIMER_RUNNING).Should().BeFalse();

            CommandHeaderByte.START_REACTION_TIMING_GAME.GetExpectedStatusLogic()(StatusByte.STARTUP_SEQUENCING).Should().BeFalse();
        }

        [Fact]
        public void GetExpectedStatusLogic_StatusRequest()
        {
            CommandHeaderByte.STATUS_REQUEST.GetExpectedStatusLogic()(StatusByte.IDLE_MODE).Should().BeTrue();

            CommandHeaderByte.STATUS_REQUEST.GetExpectedStatusLogic()(StatusByte.GAME_PRESTART).Should().BeTrue();

            CommandHeaderByte.STATUS_REQUEST.GetExpectedStatusLogic()(StatusByte.GAME_RUNNING).Should().BeTrue();

            CommandHeaderByte.STATUS_REQUEST.GetExpectedStatusLogic()(StatusByte.PERSON_BUZZED_IN).Should().BeTrue();

            CommandHeaderByte.STATUS_REQUEST.GetExpectedStatusLogic()(StatusByte.GAME_DONE).Should().BeTrue();

            CommandHeaderByte.STATUS_REQUEST.GetExpectedStatusLogic()(StatusByte.RUNNING_5_SEC_TIMER).Should().BeTrue();

            CommandHeaderByte.STATUS_REQUEST.GetExpectedStatusLogic()(StatusByte.EXTENDED_TIMER_RUNNING).Should().BeTrue();

            CommandHeaderByte.STATUS_REQUEST.GetExpectedStatusLogic()(StatusByte.STARTUP_SEQUENCING).Should().BeTrue();
        }

        [Fact]
        public void GetExpectedStatusLogic_Start5SecondTimer()
        {
            CommandHeaderByte.START_5_SEC_TIMER.GetExpectedStatusLogic()(StatusByte.IDLE_MODE).Should().BeFalse();

            CommandHeaderByte.START_5_SEC_TIMER.GetExpectedStatusLogic()(StatusByte.GAME_PRESTART).Should().BeFalse();

            CommandHeaderByte.START_5_SEC_TIMER.GetExpectedStatusLogic()(StatusByte.GAME_RUNNING).Should().BeFalse();

            CommandHeaderByte.START_5_SEC_TIMER.GetExpectedStatusLogic()(StatusByte.PERSON_BUZZED_IN).Should().BeFalse();

            CommandHeaderByte.START_5_SEC_TIMER.GetExpectedStatusLogic()(StatusByte.GAME_DONE).Should().BeFalse();

            CommandHeaderByte.START_5_SEC_TIMER.GetExpectedStatusLogic()(StatusByte.RUNNING_5_SEC_TIMER).Should().BeTrue();

            CommandHeaderByte.START_5_SEC_TIMER.GetExpectedStatusLogic()(StatusByte.EXTENDED_TIMER_RUNNING).Should().BeFalse();

            CommandHeaderByte.START_5_SEC_TIMER.GetExpectedStatusLogic()(StatusByte.STARTUP_SEQUENCING).Should().BeFalse();
        }

        [Fact]
        public void GetExpectedStatusLogic_Start30SecondTimer()
        {
            CommandHeaderByte.START_30_SEC_TIMER.GetExpectedStatusLogic()(StatusByte.IDLE_MODE).Should().BeFalse();

            CommandHeaderByte.START_30_SEC_TIMER.GetExpectedStatusLogic()(StatusByte.GAME_PRESTART).Should().BeFalse();

            CommandHeaderByte.START_30_SEC_TIMER.GetExpectedStatusLogic()(StatusByte.GAME_RUNNING).Should().BeFalse();

            CommandHeaderByte.START_30_SEC_TIMER.GetExpectedStatusLogic()(StatusByte.PERSON_BUZZED_IN).Should().BeFalse();

            CommandHeaderByte.START_30_SEC_TIMER.GetExpectedStatusLogic()(StatusByte.GAME_DONE).Should().BeFalse();

            CommandHeaderByte.START_30_SEC_TIMER.GetExpectedStatusLogic()(StatusByte.RUNNING_5_SEC_TIMER).Should().BeFalse();

            CommandHeaderByte.START_30_SEC_TIMER.GetExpectedStatusLogic()(StatusByte.EXTENDED_TIMER_RUNNING).Should().BeTrue();

            CommandHeaderByte.START_30_SEC_TIMER.GetExpectedStatusLogic()(StatusByte.STARTUP_SEQUENCING).Should().BeFalse();
        }

        [Fact]
        public void GetExpectedStatusLogic_Start1MinuteTimer()
        {
            CommandHeaderByte.START_1_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.IDLE_MODE).Should().BeFalse();

            CommandHeaderByte.START_1_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.GAME_PRESTART).Should().BeFalse();

            CommandHeaderByte.START_1_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.GAME_RUNNING).Should().BeFalse();

            CommandHeaderByte.START_1_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.PERSON_BUZZED_IN).Should().BeFalse();

            CommandHeaderByte.START_1_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.GAME_DONE).Should().BeFalse();

            CommandHeaderByte.START_1_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.RUNNING_5_SEC_TIMER).Should().BeFalse();

            CommandHeaderByte.START_1_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.EXTENDED_TIMER_RUNNING).Should().BeTrue();

            CommandHeaderByte.START_1_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.STARTUP_SEQUENCING).Should().BeFalse();
        }

        [Fact]
        public void GetExpectedStatusLogic_Start2MinuteTimer()
        {
            CommandHeaderByte.START_2_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.IDLE_MODE).Should().BeFalse();

            CommandHeaderByte.START_2_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.GAME_PRESTART).Should().BeFalse();

            CommandHeaderByte.START_2_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.GAME_RUNNING).Should().BeFalse();

            CommandHeaderByte.START_2_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.PERSON_BUZZED_IN).Should().BeFalse();

            CommandHeaderByte.START_2_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.GAME_DONE).Should().BeFalse();

            CommandHeaderByte.START_2_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.RUNNING_5_SEC_TIMER).Should().BeFalse();

            CommandHeaderByte.START_2_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.EXTENDED_TIMER_RUNNING).Should().BeTrue();

            CommandHeaderByte.START_2_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.STARTUP_SEQUENCING).Should().BeFalse();
        }

        [Fact]
        public void GetExpectedStatusLogic_Start3MinuteTimer()
        {
            CommandHeaderByte.START_3_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.IDLE_MODE).Should().BeFalse();

            CommandHeaderByte.START_3_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.GAME_PRESTART).Should().BeFalse();

            CommandHeaderByte.START_3_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.GAME_RUNNING).Should().BeFalse();

            CommandHeaderByte.START_3_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.PERSON_BUZZED_IN).Should().BeFalse();

            CommandHeaderByte.START_3_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.GAME_DONE).Should().BeFalse();

            CommandHeaderByte.START_3_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.RUNNING_5_SEC_TIMER).Should().BeFalse();

            CommandHeaderByte.START_3_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.EXTENDED_TIMER_RUNNING).Should().BeTrue();

            CommandHeaderByte.START_3_MIN_TIMER.GetExpectedStatusLogic()(StatusByte.STARTUP_SEQUENCING).Should().BeFalse();
        }

        [Fact]
        public void GetExpectedStatusLogic_StartIndefiniteTimer()
        {
            CommandHeaderByte.START_INFINITE_TIMER.GetExpectedStatusLogic()(StatusByte.IDLE_MODE).Should().BeFalse();

            CommandHeaderByte.START_INFINITE_TIMER.GetExpectedStatusLogic()(StatusByte.GAME_PRESTART).Should().BeFalse();

            CommandHeaderByte.START_INFINITE_TIMER.GetExpectedStatusLogic()(StatusByte.GAME_RUNNING).Should().BeFalse();

            CommandHeaderByte.START_INFINITE_TIMER.GetExpectedStatusLogic()(StatusByte.PERSON_BUZZED_IN).Should().BeFalse();

            CommandHeaderByte.START_INFINITE_TIMER.GetExpectedStatusLogic()(StatusByte.GAME_DONE).Should().BeFalse();

            CommandHeaderByte.START_INFINITE_TIMER.GetExpectedStatusLogic()(StatusByte.RUNNING_5_SEC_TIMER).Should().BeFalse();

            CommandHeaderByte.START_INFINITE_TIMER.GetExpectedStatusLogic()(StatusByte.EXTENDED_TIMER_RUNNING).Should().BeTrue();

            CommandHeaderByte.START_INFINITE_TIMER.GetExpectedStatusLogic()(StatusByte.STARTUP_SEQUENCING).Should().BeFalse();
        }

        [Fact]
        public void GetExpectedStatusLogic_EndIndefiniteTimer()
        {
            CommandHeaderByte.END_INFINITE_TIMER_BUZZ.GetExpectedStatusLogic()(StatusByte.IDLE_MODE).Should().BeTrue();

            CommandHeaderByte.END_INFINITE_TIMER_BUZZ.GetExpectedStatusLogic()(StatusByte.GAME_PRESTART).Should().BeTrue();

            CommandHeaderByte.END_INFINITE_TIMER_BUZZ.GetExpectedStatusLogic()(StatusByte.GAME_RUNNING).Should().BeTrue();

            CommandHeaderByte.END_INFINITE_TIMER_BUZZ.GetExpectedStatusLogic()(StatusByte.PERSON_BUZZED_IN).Should().BeTrue();

            CommandHeaderByte.END_INFINITE_TIMER_BUZZ.GetExpectedStatusLogic()(StatusByte.GAME_DONE).Should().BeTrue();

            CommandHeaderByte.END_INFINITE_TIMER_BUZZ.GetExpectedStatusLogic()(StatusByte.RUNNING_5_SEC_TIMER).Should().BeTrue();

            CommandHeaderByte.END_INFINITE_TIMER_BUZZ.GetExpectedStatusLogic()(StatusByte.EXTENDED_TIMER_RUNNING).Should().BeTrue();

            CommandHeaderByte.END_INFINITE_TIMER_BUZZ.GetExpectedStatusLogic()(StatusByte.STARTUP_SEQUENCING).Should().BeTrue();
        }

        [Fact]
        public void GetExpectedStatusLogic_Invalid()
        {
            var ex = Record.Exception(() =>
            {
                var _ = ((CommandHeaderByte)0xFF).GetExpectedStatusLogic();
            });

            ex.Should().BeOfType<InvalidOperationException>();
        }
    }
}
