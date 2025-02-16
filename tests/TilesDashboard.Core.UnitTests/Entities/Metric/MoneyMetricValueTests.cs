using FluentAssertions;
using NUnit.Framework;
using System;
using TilesDashboard.Core.UnitTests.TestData;
using TilesDashboard.V2.Core.Entities.Metric;

namespace TilesDashboard.Core.UnitTests.Entities.Metric
{
    internal class MoneyMetricValueTests
    {
        [Test]
        public void ShouldSetValue()
        {
            // given & when
            var moneyValue = new MoneyMetricValue(5, DateTimeOffsetTestData.April02Year2020);

            // then
            moneyValue.Value.Should().Be(5m);
        }

        [Test]
        public void ShouldThrowException_WhenValueIsBelowZero()
        {
            // given & when
            // ReSharper disable once ObjectCreationAsStatement - needed by test
            Action action = () => new MoneyMetricValue(-1, DateTimeOffsetTestData.April02Year2020);

            // then
            action.Should().Throw<ArgumentOutOfRangeException>().And.Message.Contains("-1", StringComparison.InvariantCultureIgnoreCase);
        }

        [Test]
        public void ShouldThrowException_WhenValueIsZero()
        {
            // given & when
            // ReSharper disable once ObjectCreationAsStatement - needed by test
            Action action = () => new MoneyMetricValue(0, DateTimeOffsetTestData.April02Year2020);

            // then
            action.Should().Throw<ArgumentOutOfRangeException>().And.Message.Contains("0", StringComparison.InvariantCultureIgnoreCase);
        }

        [Test]
        public void ShouldThrowException_WhenValueIsAboveMax()
        {
            // given & when
            // ReSharper disable once ObjectCreationAsStatement - needed by test
            Action action = () => new MoneyMetricValue(1000000, DateTimeOffsetTestData.April02Year2020);

            // then
            action.Should().Throw<ArgumentOutOfRangeException>().And.Message.Contains("1000000", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
