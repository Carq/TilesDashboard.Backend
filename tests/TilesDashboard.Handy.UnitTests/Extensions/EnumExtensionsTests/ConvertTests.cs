using TilesDashboard.TestUtils.TestClass;
using TilesDashboard.Handy.Extensions;
using NUnit.Framework;
using FluentAssertions;
using System;

namespace TilesDashboard.Handy.UnitTests.Extensions.EnumExtensionsTests
{
    internal class ConvertTests
    {
        [Test]
        public void ShouldReturnConvertToOtherEnum()
        {
            // given
            var sourceEnum = DummyTestEnum.Two;

            // when
            var result = sourceEnum.Convert<DummySecondTestEnum>();

            // then
            result.Should().Be(DummySecondTestEnum.Two);
        }

        [Test]
        public void ShouldThrowException_WhenEnumMemberIsNotInDestinationEnum()
        {

            // given
            var sourceEnum = DummyTestEnum.DummyOne;

            // given & when
            Action action = () => sourceEnum.Convert<DummySecondTestEnum>();

            // then
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void ShouldConvertStringToEnum()
        {
            // given
            var enumAsString = DummyTestEnum.DummyOne.ToString();

            // when
            var result = enumAsString.Convert<DummyTestEnum>();

            // then
            result.Should().Be(DummyTestEnum.DummyOne);
        }

        [Test]
        public void ShouldThrowException_WhenStringIsNotInDestinationEnum()
        {
            // given
            var enumAsString = "NotExistingEnum";

            // given & when
            Action action = () => enumAsString.Convert<DummySecondTestEnum>();

            // then
            action.Should().Throw<ArgumentException>();
        }
    }
}
