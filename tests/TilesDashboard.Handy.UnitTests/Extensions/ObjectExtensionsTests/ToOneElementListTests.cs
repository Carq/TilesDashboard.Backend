using FluentAssertions;
using NUnit.Framework;
using TilesDashboard.Handy.Extensions;

namespace TilesDashboard.Handy.UnitTests.Extensions.ObjectExtensionsTests
{
    internal class ToOneElementListTests
    {
        [Test]
        public void ShouldCreateOneElementList()
        {
            // given
            var oneElement = 55;

            // when
            var oneElementList = oneElement.ToOneElementList();

            // then
            oneElementList.Should().OnlyContain(x => x == 55);
        }

        [Test]
        public void ShouldCreateOneElementList_WhenObjectIsNull()
        {
            // given
            object nullElement = null;

            // when
            // ReSharper disable once ExpressionIsAlwaysNull - this is purpose of test
            var oneElementList = nullElement.ToOneElementList();

            // then
            oneElementList.Should().OnlyContain(x => x == null);
        }
    }
}
