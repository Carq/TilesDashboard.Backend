using NUnit.Framework;
using System;
using System.Collections.Generic;
using TilesDashboard.Handy.Extensions;
using FluentAssertions;
using System.Linq;
using System.Collections.ObjectModel;

namespace TilesDashboard.Handy.UnitTests.Extensions.CollectionExtensionsTests
{
    internal class AddRangeTests
    {
        [Test]
        public void ShouldNotThrowException_WhenNullIsPassedAsParameter()
        {
            // given
            IList<int> listWithOneElement = new List<int>
            {
                1,
            };

            // when
            listWithOneElement.AddRange(null);

            // then
            listWithOneElement.Should().OnlyContain(x => x == 1);
        }

        [Test]
        public void ShouldAddTwoElementsToIList_WhenAddingElementsArePassedAsList()
        {
            // given
            IList<int> listWithTwoElements = new List<int>
            {
                1,
                2,
            };

            var elementsToAdd = new List<int>
            {
                5,
                4,
                4,
            };

            // when
            listWithTwoElements.AddRange(elementsToAdd);

            // then
            listWithTwoElements.Should().OnlyContain(
                x => new[]
                {
                    1,
                    2,
                    4,
                    5,
                }.Contains(x)).And.HaveCount(5);
        }

        [Test]
        public void ShouldAddElementsToIList_WhenAddingElementsArePassedAsArray()
        {
            // given
            IList<int> listWithTwoElements = new List<int>
            {
                1,
                2,
            };

            var elementsToAdd = new[]
            {
                5,
                4,
                4,
            };

            // when
            listWithTwoElements.AddRange(elementsToAdd);

            // then
            listWithTwoElements.Should().OnlyContain(
                x => new[]
                {
                    1,
                    2,
                    4,
                    5,
                }.Contains(x)).And.HaveCount(5);
        }

        [Test]
        public void ShouldAddElementsToCollection()
        {
            // given
            IList<int> listWithTwoElements = new Collection<int>(new List<int>
            {
                1,
                2,
            });

            var elementsToAdd = new List<int>
            {
                5,
                4,
                4,
            };

            // when
            listWithTwoElements.AddRange(elementsToAdd);

            // then
            listWithTwoElements.Should().OnlyContain(
                x => new[]
                {
                    1,
                    2,
                    4,
                    5,
                }.Contains(x)).And.HaveCount(5);
        }

        [Test]
        public void ShouldThrowException_WhenAddingToNullObject()
        {
            // when
            Action action = () => ((IList<int>)null).AddRange(null);

            // then
            action.Should().Throw<ArgumentNullException>();
        }
    }
}
