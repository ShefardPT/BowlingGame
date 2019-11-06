using BowlingGame.Core.Interfaces;
using BowlingGame.Core.Models;
using NUnit.Framework;
using System;
using System.Linq;
using TypeMock.ArrangeActAssert;

namespace BowlingGame.Tests
{
    [TestFixture]
    public class TraditionalScoreBoardTests
    {
        IScoreBoard _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new TraditionalScoreBoard();            
        }

        [Test, Isolated]
        public void Should_get_300_points_with_all_strikes()
        {
            while (_sut.IsRollAllowed)
            {
                _sut.DoRoll(10);
            }

            var result = _sut.Scores.Sum();
            Assert.AreEqual(300, result);
        }

        [Test, Isolated]
        public void Should_get_0_points_with_all_misses()
        {
            while (_sut.IsRollAllowed)
            {
                _sut.DoRoll(0);
            }

            var result = _sut.Scores.Sum();
            Assert.AreEqual(0, result);
        }

        [Test, Isolated]
        public void Should_get_20_points_with_one_pin_per_ball()
        {
            while (_sut.IsRollAllowed)
            {
                _sut.DoRoll(1);
            }

            var result = _sut.Scores.Sum();
            Assert.AreEqual(20, result);
        }

        [Test, Isolated]
        public void Should_get_16_points_with_one_spare_3_pins()
        {
            _sut.DoRoll(4);
            _sut.DoRoll(6);
            _sut.DoRoll(3);

            while (_sut.IsRollAllowed)
            {
                _sut.DoRoll(0);
            }

            var result = _sut.Scores.Sum();
            Assert.AreEqual(16, result);
        }

        [Test, Isolated]
        public void Should_get_24_points_with_strike_3_pins_4_pins()
        {
            _sut.DoRoll(10);
            _sut.DoRoll(3);
            _sut.DoRoll(4);

            while (_sut.IsRollAllowed)
            {
                _sut.DoRoll(0);
            }

            var result = _sut.Scores.Sum();
            Assert.AreEqual(24, result);
        }

        [Test, Isolated]
        public void Should_throw_exception_if_rolling_when_not_allowed()
        {
            var random = new Random();

            while (_sut.IsRollAllowed)
            {
                RollRandomly(random);
            }

            try
            {
                _sut.DoRoll(random.Next(11));
            }
            catch
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test, Isolated]
        public void Should_roll_once_more_in_case_of_spare_on_10th_frame()
        {
            var random = new Random();

            while (_sut.FrameIndex < 9)
            {
                RollRandomly(random);
            }

            // Spare on 10th frame
            _sut.DoRoll(2);
            _sut.DoRoll(8);

            // Rolling once more
            _sut.DoRoll(random.Next(11));

            try
            {
                _sut.DoRoll(random.Next(11));
            }
            catch
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test, Isolated]
        public void Should_roll_twice_more_in_case_of_strike_on_10th_frame()
        {
            var random = new Random();

            while (_sut.FrameIndex < 9)
            {
                RollRandomly(random);
            }

            // Strike on 10th frame
            _sut.DoRoll(10);

            // Rolling twice more
            var firstRoll = random.Next(11);
            _sut.DoRoll(firstRoll);

            var secondRoll = 0;

            if (firstRoll == 10)
                secondRoll = random.Next(11);
            else
                secondRoll = random.Next(10 - firstRoll);

            _sut.DoRoll(secondRoll);

            try
            {
                _sut.DoRoll(random.Next(10));
            }
            catch
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        private void RollRandomly(Random random)
        {
            var firstRoll = random.Next(11);
            _sut.DoRoll(firstRoll);

            if (firstRoll == 10)
                return;

            var secondRoll = random.Next(11 - firstRoll);
            _sut.DoRoll(secondRoll);
        }
    }
}