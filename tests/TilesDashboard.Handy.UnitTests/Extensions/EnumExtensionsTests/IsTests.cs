using NUnit.Framework;
using FluentAssertions;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.TestUtils.TestClass;

namespace TilesDashboard.Handy.UnitTests.Extensions.EnumExtensionsTests
{
    internal class IsTests
    {
        [Test]
        public void ShouldReturnFalse_WhenEnumsHaveDifferentValues()
        {
            // given
            var sourceEnum = DummyTestEnum.Two;

            // when && then
            sourceEnum.Is(DummyTestEnum.DummyOne).Should().BeFalse();
        }

        [Test]
        public void ShouldReturnTrue_WhenEnumsHaveTheSameValue()
        {
            // given
            var sourceEnum = DummyTestEnum.Two;

            // when && then
            sourceEnum.Is(DummyTestEnum.Two).Should().BeTrue();
        }
    }
}
