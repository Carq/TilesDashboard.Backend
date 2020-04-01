using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TilesDashboard.Core.Domain.Entities;
using TilesDashboard.Core.Domain.Enums;
using TilesDashboard.Core.Domain.ValueObjects;
using TilesDashboard.Core.Entities;
using TilesDashboard.Core.Exceptions;
using TilesDashboard.Core.Storage;
using TilesDashboard.Core.Storage.Entities;
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
            var tileDbEntity = await _context.GetTiles().Find(x => x.Id.Name.ToLowerInvariant() == tileName.ToLowerInvariant() && x.Id.TileType == TileType.Weather).SingleOrDefaultAsync(token);
            if (tileDbEntity.NotExists())
            {
                throw new NotFoundException($"Tile {tileName} does not exist.");
            }

            var rawWeatherData = tileDbEntity.Data.OrderBy(x => x[nameof(TileData.AddedOn)]).Last();
            return BsonSerializer.Deserialize<WeatherData>(rawWeatherData);
        }

        public async Task RecordWeatherDataAsync(string tileName, Temperature temperature, Percentage huminidy, DateTimeOffset? dateOfChange, CancellationToken token)
        {
            var weatherData = new WeatherData(temperature, huminidy, dateOfChange ?? _dateTimeOffsetProvider.Now);
            var tileExists = await _context.GetTiles().Find(Filter(tileName)).AnyAsync(token);
            if (!tileExists)
            {
                throw new NotFoundException($"Tile {tileName} does not exist.");
            }

            await _context.GetTiles().UpdateOneAsync(
                Filter(tileName),
                Builders<TileDbEntity>.Update.Push(x => x.Data, weatherData.ToBsonDocument()),
                null,
                token);
        }

        private FilterDefinition<TileDbEntity> Filter(string tileName)
        {
            return Builders<TileDbEntity>.Filter.And(
                    Builders<TileDbEntity>.Filter.Eq(x => x.Id.Name, tileName),
                    Builders<TileDbEntity>.Filter.Eq(x => x.Id.TileType, TileType.Weather));
        }
    }
}
