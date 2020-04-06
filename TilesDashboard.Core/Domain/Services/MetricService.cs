using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TilesDashboard.Core.Domain.Entities;
using TilesDashboard.Core.Domain.Enums;
using TilesDashboard.Core.Entities;
using TilesDashboard.Core.Exceptions;
using TilesDashboard.Core.Storage;
using TilesDashboard.Core.Storage.Entities;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.Handy.Tools;

namespace TilesDashboard.Core.Domain.Services
{
    public class MetricService : TileService, IMetricService
    {
        private readonly ITileContext _context;

        private readonly IDateTimeOffsetProvider _dateTimeOffsetProvider;

        public MetricService(ITileContext context, IDateTimeOffsetProvider dateTimeOffsetProvider)
            : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dateTimeOffsetProvider = dateTimeOffsetProvider ?? throw new ArgumentNullException(nameof(dateTimeOffsetProvider));
        }

        public async Task<IList<MetricData>> GetMetricRecentDataAsync(string tileName, int amountOfData, CancellationToken token)
        {
            return await GetRecentDataAsync<MetricData>(tileName, TileType.Metric, amountOfData, token);
        }

        public async Task RecordMetricDataAsync(string tileName, MetricType metricType, decimal currentValue, CancellationToken token)
        {
            var metricData = new MetricData(currentValue, metricType, _dateTimeOffsetProvider.Now);

            var tile = await _context.GetTiles().Find(Filter(tileName)).SingleOrDefaultAsync(token);
            if (tile.NotExists())
            {
                throw new NotFoundException($"Tile {tileName} does not exist.");
            }

            var metricConfiguration = BsonSerializer.Deserialize<MetricConfiguration>(tile.Configuration);
            if (metricConfiguration.MetricType != metricType)
            {
                throw new NotSupportedOperationException($"Metric {tileName} is type of {metricConfiguration.MetricType} but type {metricType} has been passed.");
            }

            await _context.GetTiles().UpdateOneAsync(
               Filter(tileName),
               Builders<TileDbEntity>.Update.Push(x => x.Data, metricData.ToBsonDocument()),
               null,
               token);
        }

        private FilterDefinition<TileDbEntity> Filter(string tileName)
        {
            return Builders<TileDbEntity>.Filter.And(
                    Builders<TileDbEntity>.Filter.Eq(x => x.Id.Name, tileName),
                    Builders<TileDbEntity>.Filter.Eq(x => x.Id.TileType, TileType.Metric));
        }
    }
}
