using System;

namespace TilesDashboard.PluginBase.WeatherPluginBase
{
    public class WeatherData : ITileData
    {
        public WeatherData(decimal temperature, decimal huminidy, DateTimeOffset dateOfChange)
            : this(temperature, huminidy)
        {
            DateOfChange = dateOfChange;
        }

        public WeatherData(decimal temperature, decimal huminidy)
        {
            Temperature = temperature;
            Huminidy = huminidy;
        }

        public WeatherData(decimal temperature, DateTimeOffset dateOfChange)
        {
            Temperature = temperature;
            DateOfChange = dateOfChange;
        }

        public decimal Temperature { get; private set; }

        public decimal? Huminidy { get; private set; }

        public DateTimeOffset? DateOfChange { get; private set; }
    }
}
