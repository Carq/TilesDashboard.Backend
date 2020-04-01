using System;
using Dawn;
using TilesDashboard.Core.Domain.ValueObjects;
using TilesDashboard.Core.Storage.Entities;

namespace TilesDashboard.Core.Domain.Entities
{
    public class WeatherData : TileData
    {
        public WeatherData(Temperature temperature, Percentage humidity, DateTimeOffset addedOn)
            : base(addedOn)
        {
            Temperature = Guard.Argument(temperature, nameof(temperature)).NotNull();
            Humidity = humidity;
        }

        public Temperature Temperature { get; private set; }

        public Percentage Humidity { get; private set; }
    }
}
