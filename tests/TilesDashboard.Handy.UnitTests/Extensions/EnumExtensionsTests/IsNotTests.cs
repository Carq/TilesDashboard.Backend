using NUnit.Framework;
using FluentAssertions;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.TestUtils.TestClass;

namespace TilesDashboard.Handy.UnitTests.Extensions.EnumExtensionsTests
{
    internal class IsNotTests
    {
        [Test]
        public void ShouldReturnTrue_WhenEnumsHaveDifferentValues()
        {
            // given
            var sourceEnum = DummyTestEnum.Two;

            // when && then
            sourceEnum.IsNot(DummyTestEnum.DummyOne).Should().BeTrue();
        }

        [Test]
        public void ShouldReturnFalse_WhenEnumsHaveTheSameValue()
        {
            // given
            var sourceEnum = DummyTestEnum.Two;

            // when && then
            sourceEnum.IsNot(DummyTestEnum.Two).Should().BeFalse();
        }
    }
}
