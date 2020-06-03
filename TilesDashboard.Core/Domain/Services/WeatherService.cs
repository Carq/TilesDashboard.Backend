using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using TilesDashboard.Contract.Enums;
using TilesDashboard.Contract.Events;
using TilesDashboard.Core.Domain.Entities;
using TilesDashboard.Core.Domain.Enums;
using TilesDashboard.Core.Domain.ValueObjects;
using TilesDashboard.Core.Storage;
using TilesDashboard.Core.Storage.Entities;
using TilesDashboard.Handy.Events;
using TilesDashboard.Handy.Tools;

namespace TilesDashboard.Core.Domain.Services
{
    public class WeatherService : TileService, IWeatherServices
    {
        private readonly ITileContext _context;

        private readonly IDateTimeOffsetProvider _dateTimeOffsetProvider;

        private readonly IEventDispatcher _eventDispatcher;

        public WeatherService(ITileContext context, IDateTimeOffsetProvider dateTimeOffsetProvider, IEventDispatcher eventDispatcher)
            : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dateTimeOffsetProvider = dateTimeOffsetProvider ?? throw new ArgumentNullException(nameof(dateTimeOffsetProvider));
            _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
        }

        public async Task<IList<WeatherData>> GetWeatherRecentDataAsync(string tileName, int amountOfData, CancellationToken token)
        {
            return await GetRecentDataAsync<WeatherData>(tileName, TileType.Weather, amountOfData, token);
        }

        public async Task RecordWeatherDataAsync(string tileName, Temperature temperature, Percentage humidity, DateTimeOffset? dateOfChange, CancellationToken token)
        {
            var weatherData = new WeatherData(temperature, humidity, dateOfChange ?? _dateTimeOffsetProvider.Now);
            var result = await _context.GetTiles().UpdateOneAsync(
                Filter(tileName),
                Builders<TileDbEntity>.Update.Push(x => x.Data, weatherData.ToBsonDocument()),
                null,
                token);

            if (result.ModifiedCount > 0)
            {
                await _eventDispatcher.PublishAsync(new NewDataEvent(tileName, TileTypeDto.Weather, new { Temperature = weatherData.Temperature.GetRoundedValue(), Humidity = weatherData.Humidity.GetRoundedValue(), weatherData.AddedOn }), token);
            }
        }

        private FilterDefinition<TileDbEntity> Filter(string tileName)
        {
            return Builders<TileDbEntity>.Filter.And(
                    Builders<TileDbEntity>.Filter.Eq(x => x.Id.Name, tileName),
                    Builders<TileDbEntity>.Filter.Eq(x => x.Id.TileType, TileType.Weather));
        }
    }
}
