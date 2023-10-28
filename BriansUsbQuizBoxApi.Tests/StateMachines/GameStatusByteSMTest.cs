using BriansUsbQuizBoxApi.Protocols;
using BriansUsbQuizBoxApi.StateMachines;
using FluentAssertions;
using System;

namespace BriansUsbQuizBoxApi.Tests.StateMachines
{
    public class GameStatusByteSMTests
    {
        [Fact]
        public void Idle()
        {
            var sm = new GameStatusByteSM(() => Assert.Fail("This should not be called"),
                () => Assert.Fail("This should not be called"),
                () => Assert.Fail("This should not be called"),
                (r1, r2, r3, r4, g1, g2, g3, g4) => Assert.Fail("This should not be called"));

            sm.Process(new BoxStatusReport(StatusByte.IDLE_MODE, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
        }

        [Fact]
        public void GameStarted()
        {
            var callbackCalled = false;

            var sm = new GameStatusByteSM(() => callbackCalled = true,
                () => Assert.Fail("This should not be called"),
                () => Assert.Fail("This should not be called"),
                (r1, r2, r3, r4, g1, g2, g3, g4) => Assert.Fail("This should not be called"));

            sm.Process(new BoxStatusReport(StatusByte.GAME_PRESTART, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));

            callbackCalled.Should().BeTrue();
        }

        [Fact]
        public void GameRunning()
        {
            var callbackCalled = false;

            var sm = new GameStatusByteSM(() => { /* Don't care */ },
                () => callbackCalled = true,
                () => Assert.Fail("This should not be called"),
                (r1, r2, r3, r4, g1, g2, g3, g4) => Assert.Fail("This should not be called"));

            sm.Process(new BoxStatusReport(StatusByte.GAME_PRESTART, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
            sm.Process(new BoxStatusReport(StatusByte.GAME_RUNNING, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));

            callbackCalled.Should().BeTrue();
        }

        [Fact]
        public void GameFirstPersonBuzzedIn()
        {
            var callbackCalled = false;

            var sm = new GameStatusByteSM(() => { /* Don't care */ },
                () => { /* Don't care */ },
                () => callbackCalled = true,
                (r1, r2, r3, r4, g1, g2, g3, g4) => Assert.Fail("This should not be called"));

            sm.Process(new BoxStatusReport(StatusByte.GAME_PRESTART, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
            sm.Process(new BoxStatusReport(StatusByte.GAME_RUNNING, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
            sm.Process(new BoxStatusReport(StatusByte.PERSON_BUZZED_IN, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));

            callbackCalled.Should().BeTrue();
        }

        [Fact]
        public void GameDonWithoutAFirstBuzzIn()
        {
            var callbackCalled = false;

            var sm = new GameStatusByteSM(() => { /* Don't care */ },
                () => { /* Don't care */ },
                () => Assert.Fail("This should not be called"),
                (r1, r2, r3, r4, g1, g2, g3, g4) => callbackCalled = true);

            sm.Process(new BoxStatusReport(StatusByte.GAME_PRESTART, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
            sm.Process(new BoxStatusReport(StatusByte.GAME_RUNNING, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
            sm.Process(new BoxStatusReport(StatusByte.GAME_DONE, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));

            callbackCalled.Should().BeTrue();
        }

        [Fact]
        public void GameDone()
        {
            var callbackCalled = false;
            decimal cr1 = 0m, cr2 = 0m, cr3 = 0m, cr4 = 0m, cg1 = 0m, cg2 = 0m, cg3 = 0m, cg4 = 0m;

            var sm = new GameStatusByteSM(() => { /* Don't care */ },
                () => { /* Don't care */ },
                () => { /* Don't care */ },
                (r1, r2, r3, r4, g1, g2, g3, g4) => { callbackCalled = true;
                    cr1 = r1;
                    cr2 = r2;
                    cr3 = r3;
                    cr4 = r4;
                    cg1 = g1;
                    cg2 = g2;
                    cg3 = g3;
                    cg4 = g4;
                });

            sm.Process(new BoxStatusReport(StatusByte.GAME_PRESTART, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
            sm.Process(new BoxStatusReport(StatusByte.GAME_RUNNING, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
            sm.Process(new BoxStatusReport(StatusByte.PERSON_BUZZED_IN, WinnerByte.NO_VALID_WINNER, 0, 0, 0, 0, 0, 0, 0, 0));
            sm.Process(new BoxStatusReport(StatusByte.GAME_DONE, WinnerByte.NO_VALID_WINNER, 1.0m, 2.0m, 3.0m, 4.0m, 5.0m, 6.0m, 7.0m, 8.0m));

            callbackCalled.Should().BeTrue();
            cr1.Should().Be(1.0m);
            cr2.Should().Be(2.0m);
            cr3.Should().Be(3.0m);
            cr4.Should().Be(4.0m);
            cg1.Should().Be(5.0m);
            cg2.Should().Be(6.0m);
            cg3.Should().Be(7.0m);
            cg4.Should().Be(8.0m);
        }
    }
}
