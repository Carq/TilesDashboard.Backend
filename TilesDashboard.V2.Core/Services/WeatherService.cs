using System.Threading.Tasks;
using TilesDashboard.Handy.Tools;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Weather;
using TilesDashboard.V2.Core.Repositories;

namespace TilesDashboard.V2.Core.Services
{
    public class WeatherService : TileBaseService, IWeatherService
    {
        public WeatherService(IDateTimeProvider dateTimeProvider, ITileRepository tileRepository)
            : base(tileRepository, dateTimeProvider)
        {
        }

        public async Task<WeatherTile> GetWeatherTile(TileId tileId) => await GetTile<WeatherTile>(tileId);

        public async Task RecordValue(TileId tileId, decimal temperature, decimal humidity)
        {
            var weatherValue = new WeatherValue(temperature, humidity, DateTimeProvider.Now);
            await TileRepository.RecordValue(tileId, weatherValue);
        }

        public async Task RecordValue(TileStorageId tileStorageId, decimal temperature, decimal humidity)
        {
            var weatherValue = new WeatherValue(temperature, humidity, DateTimeProvider.Now);
            await TileRepository.RecordValue(tileStorageId, weatherValue, TileType.Weather);
        }
    }
}
