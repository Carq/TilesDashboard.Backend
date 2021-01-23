using FluentAssertions;
using NUnit.Framework;
using System;
using TilesDashboard.Core.UnitTests.TestData;
using TilesDashboard.V2.Core.Entities.Metric;

namespace TilesDashboard.Core.UnitTests.Entities.Metric
{
    internal class PercentageMetricValueTests
    {
        [TestCase(100.01, 100.0)]
        [TestCase(50.39, 50.4)]
        public void ShouldSetValue(decimal givenValue, decimal expectedValue)
        {
            // given & when
            var moneyValue = new PercentageMetricValue(givenValue, DateTimeOffsetTestData.April02Year2020);

            // then
            moneyValue.Value.Should().Be(expectedValue);
            moneyValue.AddedOn.Should().Be(DateTimeOffsetTestData.April02Year2020);
        }

        [TestCase(100.1)]
        [TestCase(-1)]
        public void ShouldThrowException_WhenValueIsOutOfRange(decimal value)
        {
            // given & when
            Action action = () => new PercentageMetricValue(value, DateTimeOffsetTestData.April02Year2020);

            // then
            action.Should().Throw<ArgumentOutOfRangeException>().And.Message.Contains(value.ToString());
        }
    }
}
