using NUnit.Framework;
using System;
using System.Collections.Generic;
using TilesDashboard.Handy.Extensions;
using FluentAssertions;

namespace TilesDashboard.Handy.UnitTests.Extensions.CollectionExtensionsTests
{
    internal class IsEmptyTests
    {
        [Test]
        public void ShouldThrowException_WhenNullListIsNull()
        {
            // given
            IList<int> listWithOneElement = null;

            // when
            Action action = () => listWithOneElement.IsEmpty();

            // then
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void ShouldReturnFalse_WhenListIsNotEmpty()
        {
            // given
            IList<int> listWithTwoElements = new List<int>
            {
                1,
                2,
            };

            // when
            var result = listWithTwoElements.IsEmpty();

            // then
            result.Should().BeFalse();
        }

        [Test]
        public void ShouldReturnTrue_WhenListIsEmpty()
        {
            // given
            IList<int> listWithTwoElements = new List<int>();

            // when
            var result = listWithTwoElements.IsEmpty();

            // then
            result.Should().BeTrue();
        }

        [Test]
        public void ShouldReturnFalse_WhenArrayIsNotEmpty()
        {
            // given
            IList<int> listWithTwoElements = new[]
            {
                1,
                2,
            };

            // when
            var result = listWithTwoElements.IsEmpty();

            // then
            result.Should().BeFalse();
        }

        [Test]
        public void ShouldReturnTrue_WhenArrayIsEmpty()
        {
            // given
            int[] listWithTwoElements = new int[0];

            // when
            var result = listWithTwoElements.IsEmpty();

            // then
            result.Should().BeTrue();
        }
    }
}
