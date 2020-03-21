using Dawn;
using TilesDashboard.Core.Domain.Enums;
using TilesDashboard.Core.Domain.ValueObjects;

namespace TilesDashboard.Core.Domain.Entities
{
    public class WeatherTile : TileBase
    {
        public WeatherTile(string tileName, Temperature temperature, Humidity humidity)
            : base(tileName, TileType.Weather)
        {
            Temperature = Guard.Argument(temperature, nameof(temperature)).NotNull();
            Humidity = Guard.Argument(humidity, nameof(humidity)).NotNull();
        }

        public Temperature Temperature { get; private set; }

        public Humidity Humidity { get; private set; }
    }
}
