using System;

namespace TilesDashboard.PluginBase.WeatherPluginBase
{
    public interface ITileData
    {
        public DateTimeOffset? DateOfChange { get; }
    }
}
