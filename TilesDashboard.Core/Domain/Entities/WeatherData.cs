using System;
using Dawn;
using TilesDashboard.Core.Domain.ValueObjects;
using TilesDashboard.Core.Entities;

namespace TilesDashboard.Core.Domain.Entities
{
    public class WeatherData : TileData
    {
        public WeatherData(Temperature temperature, Humidity humidity, DateTimeOffset addedOn)
            : base(addedOn)
        {
            Temperature = Guard.Argument(temperature, nameof(temperature)).NotNull();
            Humidity = Guard.Argument(humidity, nameof(humidity)).NotNull();
        }

        public Temperature Temperature { get; private set; }

        public Humidity Humidity { get; private set; }
    }
}
