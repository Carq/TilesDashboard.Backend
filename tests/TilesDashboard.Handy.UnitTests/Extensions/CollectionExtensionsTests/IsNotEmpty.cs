using NUnit.Framework;
using System;
using System.Collections.Generic;
using TilesDashboard.Handy.Extensions;
using FluentAssertions;

namespace TilesDashboard.Handy.UnitTests.Extensions.CollectionExtensionsTests
{
    internal class IsNotEmpty
    {
        [Test]
        public void ShouldThrowException_WhenNullListIsNull()
        {
            // given
            IList<int> listWithOneElement = null;

            // when
            // ReSharper disable once ExpressionIsAlwaysNull - needed by test
            Action action = () => listWithOneElement.IsNotEmpty();

            // then
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void ShouldReturnTrue_WhenListIsNotEmpty()
        {
            // given
            IList<int> listWithTwoElements = new List<int>
            {
                1,
                2,
            };

            // when
            var result = listWithTwoElements.IsNotEmpty();

            // then
            result.Should().BeTrue();
        }

        [Test]
        public void ShouldReturnFalse_WhenListIsEmpty()
        {
            // given
            IList<int> listWithTwoElements = new List<int>();

            // when
            var result = listWithTwoElements.IsNotEmpty();

            // then
            result.Should().BeFalse();
        }

        [Test]
        public void ShouldReturnTrue_WhenArrayIsNotEmpty()
        {
            // given
            IList<int> listWithTwoElements = new[]
            {
                1,
                2,
            };

            // when
            var result = listWithTwoElements.IsNotEmpty();

            // then
            result.Should().BeTrue();
        }

        [Test]
        public void ShouldReturnFalse_WhenArrayIsEmpty()
        {
            // given
            int[] listWithTwoElements = new int[0];

            // when
            var result = listWithTwoElements.IsNotEmpty();

            // then
            result.Should().BeFalse();
        }
    }
}
