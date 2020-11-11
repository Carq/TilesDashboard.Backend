using FluentAssertions;
using NUnit.Framework;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.TestUtils.TestClass;

namespace TilesDashboard.Handy.UnitTests.Extensions.ObjectExtensionsTests
{
    public class NotExistsTests
    {
        [Test]
        public void ShouldReturnFalse_WhenObjectIsReferenceTypeAndItIsNotNull()
        {
            // given
            var referenceType = new DummyTestClass(1, "test", DummyTestEnum.Two);

            // when
            var result = referenceType.NotExists();

            // then
            result.Should().BeFalse();
        }

        [Test]
        public void ShouldReturnTrue_WhenObjectIsNull()
        {
            // given
            var referenceType = (DummyTestClass)null;

            // when
            // ReSharper disable once ExpressionIsAlwaysNull - this is purpose of test
            var result = referenceType.NotExists();

            // then
            result.Should().BeTrue();
        }

        [Test]
        public void ShouldReturnTrue_WhenObjectIsValueTypeAndItIsNull()
        {
            // given
            int? valueType = null;

            // when
            // ReSharper disable once ExpressionIsAlwaysNull - this is purpose of test
            var result = valueType.NotExists();

            // then
            result.Should().BeTrue();
        }
    }
}
