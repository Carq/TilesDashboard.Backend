using System.Collections.Generic;
using TilesDashboard.Handy.Tools;
using TilesDashboard.TestUtils.TestClassBuilder;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Metric;
using TilesDashboard.V2.Core.Entities.Weather;

namespace TilesDashboard.Core.UnitTests.TestClassBuilders
{
    internal class WeatherTileTestBuilder : TestClassBuilder<WeatherTile>
    {
        private readonly IDictionary<string, object> _configuration = new Dictionary<string, object>();

        public static WeatherTileTestBuilder New(string tileName = "weatherNameName")  => new WeatherTileTestBuilder(tileName);

        private WeatherTileTestBuilder(string tileName)
        {
            Item = new WeatherTile();
            PrivatePropertySetter.SetPropertyWithNoSetter(Item, nameof(MetricTile.TileId), new TileId(tileName, TileType.Weather));
            PrivatePropertySetter.SetPropertyWithNoSetter(Item, "TileConfiguration", _configuration);
        }

        public WeatherTileTestBuilder WithConfigEntry(string key, object value)
        {
            _configuration.Add(key, value);
            return this;
        }
    }
}
