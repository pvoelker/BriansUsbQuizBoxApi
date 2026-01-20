using BriansUsbQuizBoxApi.Protocols;
using BriansUsbQuizBoxApi.StateMachines;
using FluentAssertions;
using System;

namespace BriansUsbQuizBoxApi.Tests.StateMachines
{
    /// <summary>
    /// Game status byte state machine tests for version 1 protocol
    /// </summary>
    public class GameStatusByteSMTestsV1
    {
        [Fact]
        public void Idle()
        {
            var sm = new GameStatusByteSM(() => Assert.Fail("This should not be called"),
                () => Assert.Fail("This should not be called"),
                (p1, p2, p3, p4, p5, p6, p7, p8, r1, r2, r3, r4, g1, g2, g3, g4) => Assert.Fail("This should not be called"),
                (p1, p2, p3, p4, p5, p6, p7, p8, r1, r2, r3, r4, g1, g2, g3, g4) => Assert.Fail("This should not be called"));

            sm.Process(new BoxStatusReport(StatusByte.IDLE_MODE,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                0, 0, 0, 0, 0, 0, 0, 0));
        }

        [Fact]
        public void GameStarted()
        {
            var callbackCalled = false;

            var sm = new GameStatusByteSM(() => callbackCalled = true,
                () => Assert.Fail("This should not be called"),
                (p1, p2, p3, p4, p5, p6, p7, p8, r1, r2, r3, r4, g1, g2, g3, g4) => Assert.Fail("This should not be called"),
                (p1, p2, p3, p4, p5, p6, p7, p8, r1, r2, r3, r4, g1, g2, g3, g4) => Assert.Fail("This should not be called"));

            sm.Process(new BoxStatusReport(StatusByte.GAME_PRESTART,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                0, 0, 0, 0, 0, 0, 0, 0));

            callbackCalled.Should().BeTrue();
        }

        [Fact]
        public void GameRunning()
        {
            var callbackCalled = false;

            var sm = new GameStatusByteSM(() => { /* Don't care */ },
                () => callbackCalled = true,
                (p1, p2, p3, p4, p5, p6, p7, p8, r1, r2, r3, r4, g1, g2, g3, g4) => Assert.Fail("This should not be called"),
                (p1, p2, p3, p4, p5, p6, p7, p8, r1, r2, r3, r4, g1, g2, g3, g4) => Assert.Fail("This should not be called"));

            sm.Process(new BoxStatusReport(StatusByte.GAME_PRESTART,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                0, 0, 0, 0, 0, 0, 0, 0));
            sm.Process(new BoxStatusReport(StatusByte.GAME_RUNNING,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                0, 0, 0, 0, 0, 0, 0, 0));

            callbackCalled.Should().BeTrue();
        }

        [Fact]
        public void GameDoneWithoutAFirstBuzzIn()
        {
            var callbackCalled = false;

            var sm = new GameStatusByteSM(() => { /* Don't care */ },
                () => { /* Don't care */ },
                (p1, p2, p3, p4, p5, p6, p7, p8, r1, r2, r3, r4, g1, g2, g3, g4) => callbackCalled = true,
                (p1, p2, p3, p4, p5, p6, p7, p8, r1, r2, r3, r4, g1, g2, g3, g4) => Assert.Fail("This should not be called"));

            sm.Process(new BoxStatusReport(StatusByte.GAME_PRESTART,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                0, 0, 0, 0, 0, 0, 0, 0));
            sm.Process(new BoxStatusReport(StatusByte.GAME_RUNNING,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                0, 0, 0, 0, 0, 0, 0, 0));
            sm.Process(new BoxStatusReport(StatusByte.GAME_DONE,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                0, 0, 0, 0, 0, 0, 0, 0));

            callbackCalled.Should().BeTrue();
        }

        [Fact]
        public void GameDone()
        {
            var callbackCalled = false;
            Paddle? pd1 = null, pd2 = null, pd3 = null, pd4 = null, pd5 = null, pd6 = null, pd7 = null, pd8 = null;
            decimal? cr1 = 0m, cr2 = 0m, cr3 = 0m, cr4 = 0m, cg1 = 0m, cg2 = 0m, cg3 = 0m, cg4 = 0m;

            var sm = new GameStatusByteSM(() => { /* Don't care */ },
                () => { /* Don't care */ },
                (p1, p2, p3, p4, p5, p6, p7, p8, r1, r2, r3, r4, g1, g2, g3, g4) => { callbackCalled = true;
                    pd1 = p1;
                    pd2 = p2;
                    pd3 = p3;
                    pd4 = p4;
                    pd5 = p5;
                    pd6 = p6;
                    pd7 = p7;
                    pd8 = p8;
                    cr1 = r1;
                    cr2 = r2;
                    cr3 = r3;
                    cr4 = r4;
                    cg1 = g1;
                    cg2 = g2;
                    cg3 = g3;
                    cg4 = g4;
                },
                (p1, p2, p3, p4, p5, p6, p7, p8, r1, r2, r3, r4, g1, g2, g3, g4) => Assert.Fail("This should not be called"));

            sm.Process(new BoxStatusReport(StatusByte.GAME_PRESTART,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                0, 0, 0, 0, 0, 0, 0, 0));
            sm.Process(new BoxStatusReport(StatusByte.GAME_RUNNING,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                0, 0, 0, 0, 0, 0, 0, 0));
            sm.Process(new BoxStatusReport(StatusByte.PERSON_BUZZED_IN,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                0, 0, 0, 0, 0, 0, 0, 0));
            sm.Process(new BoxStatusReport(StatusByte.GAME_DONE,
                WinnerByte.GREEN_4, WinnerByte.RED_4, WinnerByte.GREEN_3, WinnerByte.RED_3,
                WinnerByte.GREEN_2, WinnerByte.RED_2, WinnerByte.GREEN_1, WinnerByte.RED_1,
                1.0m, 2.0m, 3.0m, 4.0m, 5.0m, 6.0m, 7.0m, null));

            callbackCalled.Should().BeTrue();
            pd1.Should().Be(Paddle.GREEN_4);
            pd2.Should().Be(Paddle.RED_4);
            pd3.Should().Be(Paddle.GREEN_3);
            pd4.Should().Be(Paddle.RED_3);
            pd5.Should().Be(Paddle.GREEN_2);
            pd6.Should().Be(Paddle.RED_2);
            pd7.Should().Be(Paddle.GREEN_1);
            pd8.Should().Be(Paddle.RED_1);
            cr1.Should().Be(1.0m);
            cr2.Should().Be(2.0m);
            cr3.Should().Be(3.0m);
            cr4.Should().Be(4.0m);
            cg1.Should().Be(5.0m);
            cg2.Should().Be(6.0m);
            cg3.Should().Be(7.0m);
            cg4.Should().BeNull();
        }

        [Fact]
        public void GameDoneWithoutGame()
        {
            var callbackCalled = false;
            Paddle? pd1 = null, pd2 = null, pd3 = null, pd4 = null, pd5 = null, pd6 = null, pd7 = null, pd8 = null;
            decimal? cr1 = null, cr2 = null, cr3 = null, cr4 = null, cg1 = null, cg2 = null, cg3 = null, cg4 = null;

            var sm = new GameStatusByteSM(() => Assert.Fail("This should not be called"),
                () => Assert.Fail("This should not be called"),
                (p1, p2, p3, p4, p5, p6, p7, p8, r1, r2, r3, r4, g1, g2, g3, g4) => Assert.Fail("This should not be called"),
                (p1, p2, p3, p4, p5, p6, p7, p8, r1, r2, r3, r4, g1, g2, g3, g4) => {
                    callbackCalled = true;
                    pd1 = p1;
                    pd2 = p2;
                    pd3 = p3;
                    pd4 = p4;
                    pd5 = p5;
                    pd6 = p6;
                    pd7 = p7;
                    pd8 = p8;
                    cr1 = r1;
                    cr2 = r2;
                    cr3 = r3;
                    cr4 = r4;
                    cg1 = g1;
                    cg2 = g2;
                    cg3 = g3;
                    cg4 = g4;
                });

            sm.Process(new BoxStatusReport(StatusByte.GAME_DONE,
                WinnerByte.RED_4, WinnerByte.NO_VALID_WINNER, WinnerByte.RED_3, WinnerByte.NO_VALID_WINNER,
                WinnerByte.RED_2, WinnerByte.NO_VALID_WINNER, WinnerByte.RED_1, WinnerByte.NO_VALID_WINNER,
                null, 1.0m, null, 0, null, 1.2m, null, 999.9m));

            callbackCalled.Should().BeTrue();
            pd1.Should().Be(Paddle.RED_4);
            pd2.Should().BeNull();
            pd3.Should().Be(Paddle.RED_3);
            pd4.Should().BeNull();
            pd5.Should().Be(Paddle.RED_2);
            pd6.Should().BeNull();
            pd7.Should().Be(Paddle.RED_1);
            pd8.Should().BeNull();
            cr1.Should().BeNull();
            cr2.Should().Be(1.0m);
            cr3.Should().BeNull();
            cr4.Should().Be(0);
            cg1.Should().BeNull();
            cg2.Should().Be(1.2m);
            cg3.Should().BeNull();
            cg4.Should().Be(999.9m);
        }

        [Fact]
        public void GameDoneWithoutGameAndNoBuzzIns()
        {
            var callbackCalled = false;
            Paddle? pd1 = null, pd2 = null, pd3 = null, pd4 = null, pd5 = null, pd6 = null, pd7 = null, pd8 = null;
            decimal? cr1 = null, cr2 = null, cr3 = null, cr4 = null, cg1 = null, cg2 = null, cg3 = null, cg4 = null;

            var sm = new GameStatusByteSM(() => Assert.Fail("This should not be called"),
                () => Assert.Fail("This should not be called"),
                (p1, p2, p3, p4, p5, p6, p7, p8, r1, r2, r3, r4, g1, g2, g3, g4) => Assert.Fail("This should not be called"),
                (p1, p2, p3, p4, p5, p6, p7, p8, r1, r2, r3, r4, g1, g2, g3, g4) => {
                    callbackCalled = true;
                    pd1 = p1;
                    pd2 = p2;
                    pd3 = p3;
                    pd4 = p4;
                    pd5 = p5;
                    pd6 = p6;
                    pd7 = p7;
                    pd8 = p8;
                    cr1 = r1;
                    cr2 = r2;
                    cr3 = r3;
                    cr4 = r4;
                    cg1 = g1;
                    cg2 = g2;
                    cg3 = g3;
                    cg4 = g4;
                });

            sm.Process(new BoxStatusReport(StatusByte.GAME_DONE,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                null, null, null, null, null, null, null, null));

            callbackCalled.Should().BeTrue();
            pd1.Should().BeNull();
            pd2.Should().BeNull();
            pd3.Should().BeNull();
            pd4.Should().BeNull();
            pd5.Should().BeNull();
            pd6.Should().BeNull();
            pd7.Should().BeNull();
            pd8.Should().BeNull();
            cr1.Should().BeNull();
            cr2.Should().BeNull();
            cr3.Should().BeNull();
            cr4.Should().BeNull();
            cg1.Should().BeNull();
            cg2.Should().BeNull();
            cg3.Should().BeNull();
            cg4.Should().BeNull();
        }

        [Fact]
        public void PersonBuzzedInWithoutGame()
        {
            var sm = new GameStatusByteSM(() => Assert.Fail("This should not be called"),
                () => Assert.Fail("This should not be called"),
                (p1, p2, p3, p4, p5, p6, p7, p8, r1, r2, r3, r4, g1, g2, g3, g4) => Assert.Fail("This should not be called"),
                (p1, p2, p3, p4, p5, p6, p7, p8, r1, r2, r3, r4, g1, g2, g3, g4) => Assert.Fail("This should not be called"));

            sm.Process(new BoxStatusReport(StatusByte.PERSON_BUZZED_IN,
                WinnerByte.RED_4, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER, WinnerByte.NO_VALID_WINNER,
                0, 0, 0, 0, 0, 0, 0, 0));
        }
    }
}
