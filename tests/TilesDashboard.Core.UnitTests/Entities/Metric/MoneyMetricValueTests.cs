using FluentAssertions;
using NUnit.Framework;
using System;
using TilesDashboard.V2.Core.Entities.Metric;

namespace TilesDashboard.Core.UnitTests.Entities.Metric
{
    public class MoneyMetricValueTests
    {
        [Test]
        public void ShouldSetValue()
        {
            // given & when
            var moneyValue = new MoneyMetricValue(5, DateTimeOffset.Now);

            // then
            moneyValue.Value.Should().Be(5m);
        }

         [Test]
        public void ShouldThrowException_WhenValueIsBelowZero()
        {
            // given & when
            Action action = () => new MoneyMetricValue(-1, DateTimeOffset.Now);

            // then
            action.Should().Throw<ArgumentOutOfRangeException>().And.Message.Contains("-1");
        }
    }
}
