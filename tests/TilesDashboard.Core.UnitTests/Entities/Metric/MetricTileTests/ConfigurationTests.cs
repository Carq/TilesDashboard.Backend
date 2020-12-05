using FluentAssertions;
using NUnit.Framework;
using TilesDashboard.Core.UnitTests.TestClassBuilders;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Metric;

namespace TilesDashboard.Core.UnitTests.Entities.Metric.MetricTileTests
{
    internal class ConfigurationTests
    {
        [Test]
        public void ShouldReturnConfigurationWithDefaultValue_WhenOnlyLimitAndMetricTypeIsProvided()
        {
            // given 
            var limit = 50m;
            var metricTile = MetricTileTestBuilder.New
                .WithConfigEntry(nameof(MetricConfiguration.Limit), limit)
                .WithConfigEntry(nameof(MetricConfiguration.MetricType), MetricType.Percentage)
                .Build();

            // when
            var configuration = metricTile.GetMetricConfiguration();

            // then
            configuration.Limit.Should().Be(limit);
            configuration.MetricType.Should().Be(MetricType.Percentage);
            configuration.Goal.Should().BeNull();
            configuration.Wish.Should().BeNull();
            configuration.Unit.Should().BeNull();
            configuration.LowerIsBetter.Should().BeFalse();
        }

        [Test]
        public void ShouldReturnConfigurationWithAllPossibleValues()
        {
            // given 
            var limit = 50m;
            var goal = 60m;
            var wish = 70m;
            var lowerIsBetter = true;
            var unit = "PLN";

            var metricTile = MetricTileTestBuilder.New
                .WithConfigEntry(nameof(MetricConfiguration.Limit), limit)
                .WithConfigEntry(nameof(MetricConfiguration.Goal), goal)
                .WithConfigEntry(nameof(MetricConfiguration.Wish), wish)
                .WithConfigEntry(nameof(MetricConfiguration.Unit), unit)
                .WithConfigEntry(nameof(MetricConfiguration.LowerIsBetter), lowerIsBetter)
                .WithConfigEntry(nameof(MetricConfiguration.MetricType), MetricType.Money)
                .Build();

            // when
            var configuration = metricTile.GetMetricConfiguration();

            // then
            configuration.Limit.Should().Be(limit);

            configuration.Goal.Should().Be(goal);
            configuration.Wish.Should().Be(wish);
            configuration.Unit.Should().Be(unit);
            configuration.LowerIsBetter.Should().Be(lowerIsBetter);
            configuration.MetricType.Should().Be(MetricType.Money);
        }
    }
}
