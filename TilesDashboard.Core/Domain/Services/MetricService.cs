using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using TilesDashboard.Contract.Enums;
using TilesDashboard.Contract.Events;
using TilesDashboard.Core.Domain.Entities;
using TilesDashboard.Core.Domain.Repositories;
using TilesDashboard.Core.Exceptions;
using TilesDashboard.Core.Storage;
using TilesDashboard.Core.Type;
using TilesDashboard.Handy.Events;
using TilesDashboard.Handy.Tools;

namespace TilesDashboard.Core.Domain.Services
{
    public class MetricService : TileService, IMetricService
    {
        private readonly IEventDispatcher _eventDispatcher;

        public MetricService(ITileContext context, ITilesRepository tilesRepository, IDateTimeOffsetProvider dateTimeOffsetProvider, IEventDispatcher eventDispatcher)
            : base(context, tilesRepository, dateTimeOffsetProvider)
        {
            _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
        }

        public async Task<IList<MetricData>> GetMetricRecentDataAsync(string tileName, int amountOfData, CancellationToken token)
        {
            return await GetRecentDataAsync<MetricData>(tileName, TileType.Metric, amountOfData, token);
        }

        public async Task<IList<MetricData>> GetMetricDataSinceAsync(string tileName, int sinceDays, CancellationToken token)
        {
            return await GetDataSinceAsync<MetricData>(tileName, TileType.Metric, DateTimeOffsetProvider.Now.AddDays(-sinceDays), token);
        }

        public async Task RecordMetricDataAsync(string tileName, MetricType metricType, decimal currentValue, CancellationToken cancellationToken)
        {
            var metricData = new MetricData(currentValue, metricType, DateTimeOffsetProvider.Now);
            var tile = await TilesRepository.GetTileWithoutData(tileName, TileType.Metric, cancellationToken);

            var metricConfiguration = BsonSerializer.Deserialize<MetricConfiguration>(tile.Configuration);
            if (metricConfiguration.MetricType != metricType)
            {
                throw new NotSupportedOperationException($"Metric {tileName} is type of {metricConfiguration.MetricType} but type {metricType} has been passed.");
            }

            await TilesRepository.InsertData(tileName, TileType.Metric, metricData.ToBsonDocument(), cancellationToken);
            await _eventDispatcher.PublishAsync(new NewDataEvent(tileName, TileTypeDto.Metric, new { metricData.Value, metricData.AddedOn }), cancellationToken);
        }
    }
}
