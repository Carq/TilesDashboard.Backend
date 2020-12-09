using System;
using System.Threading.Tasks;
using TilesDashboard.Handy.Tools;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Weather;
using TilesDashboard.V2.Core.Repositories;

namespace TilesDashboard.V2.Core.Services
{
    public class WeatherService : TileBaseService, IWeatherService
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public WeatherService(IDateTimeProvider dateTimeProvider, ITileRepository tileRepository)
            : base(tileRepository)
        {
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public async Task<WeatherTile> GetWeatherTile(TileId tileId) => await GetTile<WeatherTile>(tileId);

        public async Task RecordValue(TileId tileId, decimal temperature, decimal humidity)
        {
            var weatherValue = new WeatherValue(temperature, humidity, _dateTimeProvider.Now);
            await TileRepository.RecordValue(tileId, weatherValue);
        }
    }
}
