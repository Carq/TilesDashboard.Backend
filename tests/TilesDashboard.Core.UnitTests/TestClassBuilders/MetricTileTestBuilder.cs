using System.Collections.Generic;
using TilesDashboard.Handy.Tools;
using TilesDashboard.TestUtils.TestClassBuilder;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Metric;

namespace TilesDashboard.Core.UnitTests.TestClassBuilders
{
    internal class MetricTileTestBuilder : TestClassBuilder<MetricTile>
    {
        private readonly IDictionary<string, object> _configuration = new Dictionary<string, object>();

        public static MetricTileTestBuilder New(string tileName = "metricName")  => new MetricTileTestBuilder(tileName);

        public static MetricTileTestBuilder NewPercentageMetric(string tileName = "metricName")  => new MetricTileTestBuilder(tileName);

        public static MetricTileTestBuilder NewMoneyMetric(string tileName = "metricName")  => new MetricTileTestBuilder(tileName, MetricType.Money);

        public static MetricTileTestBuilder NewTimeMetric(string tileName = "metricName")  => new MetricTileTestBuilder(tileName, MetricType.Time);

        private MetricTileTestBuilder(string tileName, MetricType metricType = MetricType.Percentage)
        {
            Item = new MetricTile();
            PrivatePropertySetter.SetPropertyWithNoSetter(Item, nameof(MetricTile.TileId), new TileId(tileName, TileType.Metric));
            PrivatePropertySetter.SetPropertyWithNoSetter(Item, "TileConfiguration", _configuration);
            _configuration[nameof(MetricConfiguration.MetricType)] = metricType;
            _configuration[nameof(MetricConfiguration.Limit)] = 50m;
        }

        public MetricTileTestBuilder WithConfigEntry(string key, object value)
        {
            _configuration[key] = value;
            return this;
        }
    }
}
