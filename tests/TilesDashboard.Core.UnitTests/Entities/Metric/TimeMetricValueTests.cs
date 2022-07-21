using FluentAssertions;
using NUnit.Framework;
using System;
using TilesDashboard.Core.UnitTests.TestData;
using TilesDashboard.V2.Core.Entities.Metric;

namespace TilesDashboard.Core.UnitTests.Entities.Metric
{
    internal class TimeMetricValueTests
    {
        [TestCase(100.012, 100.01)]
        [TestCase(50.399, 50.40)]
        public void ShouldSetValue(decimal givenValue, decimal expectedValue)
        {
            // given & when
            var moneyValue = new TimeMetricValue(givenValue, DateTimeOffsetTestData.April02Year2020);

            // then
            moneyValue.Value.Should().Be(expectedValue);
            moneyValue.AddedOn.Should().Be(DateTimeOffsetTestData.April02Year2020);
        }

        [Test]
        public void ShouldThrowException_WhenValueIsOutOfRange()
        {
            // given & when
            // ReSharper disable once ObjectCreationAsStatement - needed by test
            Action action = () => new TimeMetricValue(-1, DateTimeOffsetTestData.April02Year2020);

            // then
            action.Should().Throw<ArgumentOutOfRangeException>().And.Message.Contains("-1", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
