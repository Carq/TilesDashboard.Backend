using FluentAssertions;
using NUnit.Framework;
using TilesDashboard.Core.UnitTests.TestClassBuilders;
using TilesDashboard.V2.Core.Entities.Metric;

namespace TilesDashboard.Core.UnitTests.Entities.Metric.MetricTileTests
{
    internal class MetricTypeTests
    {
        [TestCase(MetricType.Money)]
        [TestCase(MetricType.Percentage)]
        [TestCase(MetricType.Time)]
        public void ShouldReturnMetricType(MetricType meticType)
        {
            // given 
            var limit = 50m;
            var metricTile = MetricTileTestBuilder.New()
                .WithConfigEntry(nameof(MetricConfiguration.Limit), limit)
                .WithConfigEntry(nameof(MetricConfiguration.MetricType), meticType)
                .Build();

            // when & then 
            metricTile.MetricType.Should().Be(meticType);
        }
    }
}
