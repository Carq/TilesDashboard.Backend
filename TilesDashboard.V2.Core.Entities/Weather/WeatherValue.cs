using System;

namespace TilesDashboard.V2.Core.Entities.Weather
{
    public class WeatherValue : TileValue
    {
        public const decimal MinHumidity = 0;

        public const decimal MaxHumidity = 100;

        public WeatherValue(decimal temperature, decimal humidity, DateTimeOffset addedOn)
            : base(addedOn)
        {
            Temperature = ParseAndValidateTemperature(temperature);
            Humidity = ParseAndValidateHumidity(humidity);
        }

        public decimal Temperature { get; private set; }

        public decimal Humidity { get; private set; }

        protected static decimal ParseAndValidateTemperature(decimal temperature)
        {
            return Math.Round(temperature, 1);
        }

        protected static decimal ParseAndValidateHumidity(decimal humidity)
        {
            humidity = Math.Round(humidity, 0);
            if (humidity < MinHumidity || humidity > MaxHumidity)
            {
                throw new ArgumentOutOfRangeException($"Humidity must be between 0% and 100%. Given value is {humidity}.");
            }

            return humidity;
        }
    }
}
