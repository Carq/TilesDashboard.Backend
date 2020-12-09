using System.Collections.Generic;
using TilesDashboard.TestUtils;
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

        private MetricTileTestBuilder(string tileName)
        {
            Item = new MetricTile();
            PrivatePropertySetter.SetPropertyWithNoSetter(Item, nameof(MetricTile.TileId), new TileId(tileName, TileType.Metric));
            PrivatePropertySetter.SetPropertyWithNoSetter(Item, "TileConfiguration", _configuration);
        }

        public MetricTileTestBuilder WithConfigEntry(string key, object value)
        {
            _configuration.Add(key, value);
            return this;
        }
    }
}
