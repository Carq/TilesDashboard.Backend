using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using TilesDashboard.Core.Domain.Entities;
using TilesDashboard.Core.Domain.Enums;
using TilesDashboard.Core.Domain.ValueObjects;
using TilesDashboard.Core.Entities;
using TilesDashboard.Core.Exceptions;
using TilesDashboard.Core.Storage;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.Handy.Tools;

namespace TilesDashboard.Core.Domain.Services
{
    public class WeatherService : IWeatherServices
    {
        private readonly ITileContext _context;

        private readonly IDateTimeOffsetProvider _dateTimeOffsetProvider;

        public WeatherService(ITileContext context, IDateTimeOffsetProvider dateTimeOffsetProvider)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dateTimeOffsetProvider = dateTimeOffsetProvider ?? throw new ArgumentNullException(nameof(dateTimeOffsetProvider));
        }

        public async Task<WeatherData> GetWeatherRecentDataAsync(string tileName, CancellationToken token)
        {
            var tileDbEntity = await _context.GetTiles<WeatherData>().Find(x => x.Id.Name.ToLowerInvariant() == tileName.ToLowerInvariant() && x.Id.TileType == TileType.Weather).SingleOrDefaultAsync(token);
            if (tileDbEntity.NotExists())
            {
                throw new NotFoundException($"Tile {tileName} does not exist.");
            }

            return tileDbEntity.Data.OrderBy(x => x.AddedOn).Last();
        }

        public async Task RecordWeatherDataAsync(string tileName, Temperature temperature, Percentage huminidy, CancellationToken token)
        {
            var weatherData = new WeatherData(temperature, huminidy, _dateTimeOffsetProvider.Now);
            var filter = Builders<TileDbEntity<WeatherData>>.Filter.And(
                    Builders<TileDbEntity<WeatherData>>.Filter.Eq(x => x.Id.Name, tileName),
                    Builders<TileDbEntity<WeatherData>>.Filter.Eq(x => x.Id.TileType, TileType.Weather));

            var tileExists = await _context.GetTiles<WeatherData>().Find(filter).AnyAsync(token);
            if (!tileExists)
            {
                throw new NotFoundException($"Tile {tileName} does not exist.");
            }

            await _context.GetTiles<WeatherData>().UpdateOneAsync(
                filter,
                Builders<TileDbEntity<WeatherData>>.Update.Push(x => x.Data, weatherData),
                null,
                token);
        }
    }
}
