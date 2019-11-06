using BowlingGame.Core.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TypeMock.ArrangeActAssert;

namespace BowlingGame.Tests
{
    [TestFixture]
    class ScoreFrameTests
    {
        ScoreFrame _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new ScoreFrame();
        }

        [Test, Isolated]
        public void Should_throw_exceptions_on_negative_values_assignment()
        {
            bool frameOneThrowed = false;
            bool frameTwoThrowed = false;

            try
            {
                _sut.FrameOne = -1;
            }
            catch (Exception)
            {
                frameOneThrowed = true;
            }

            try
            {
                _sut.FrameTwo = -1;
            }
            catch (Exception)
            {
                frameTwoThrowed = true;
            }

            Assert.IsTrue(frameOneThrowed);
            Assert.IsTrue(frameTwoThrowed);
        }

        [Test, Isolated]
        public void Should_throw_exceptions_on_sum_more_10()
        {
            bool frameOneThrowed = false;
            bool frameTwoThrowed = false;

            try
            {
                _sut.FrameOne = 11;
            }
            catch (Exception)
            {
                frameOneThrowed = true;
                _sut.FrameOne = 5;
            }

            try
            {
                _sut.FrameTwo = 6;
            }
            catch (Exception)
            {
                frameTwoThrowed = true;
            }

            Assert.IsTrue(frameOneThrowed);
            Assert.IsTrue(frameTwoThrowed);
        }
    }
}
