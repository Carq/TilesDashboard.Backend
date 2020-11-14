using FluentAssertions;
using NUnit.Framework;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.TestUtils.TestClass;

namespace TilesDashboard.Handy.UnitTests.Extensions.ObjectExtensionsTests
{
    internal class ExistsTests
    {
        [Test]
        public void ShouldReturnTrue_WhenObjectIsValueType()
        {
            // given
            int valueType = 55;

            // when
            var result = valueType.Exists();

            // then
            result.Should().BeTrue();
        }

        [Test]
        public void ShouldReturnTrue_WhenObjectIsReferenceTypeAndItIsNotNull()
        {
            // given
            var referenceType = new DummyTestClass(1, "Dummy", DummyTestEnum.Undefined);

            // when
            var result = referenceType.Exists();

            // then
            result.Should().BeTrue();
        }

        [Test]
        public void ShouldReturnFalse_WhenObjectIsNull()
        {
            // given
            var referenceType = (DummyTestClass)null;

            // when
            // ReSharper disable once ExpressionIsAlwaysNull - this is purpose of Fwhen
            var result = referenceType.Exists();

            // then
            result.Should().BeFalse();
        }
    }
}
