using System;

namespace TilesDashboard.PluginBase.WeatherPlugin
{
    public class WeatherData : Result
    {
        public WeatherData(decimal temperature, decimal huminidy, Status status, DateTimeOffset dateOfChange)
            : base(status, dateOfChange)
        {
            Temperature = temperature;
            Huminidy = huminidy;
        }

        public WeatherData(decimal temperature, decimal huminidy, Status status)
            : base(status)
        {
            Temperature = temperature;
            Huminidy = huminidy;
        }

        public WeatherData(decimal temperature, Status status, DateTimeOffset dateOfChange)
            : base(status, dateOfChange)
        {
            Temperature = temperature;
        }

        public decimal Temperature { get; private set; }

        public decimal? Huminidy { get; private set; }
    }
}
