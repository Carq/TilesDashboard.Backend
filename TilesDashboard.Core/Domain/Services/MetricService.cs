using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TilesDashboard.Core.Domain.Entities;
using TilesDashboard.Core.Domain.Enums;
using TilesDashboard.Core.Entities;
using TilesDashboard.Core.Exceptions;
using TilesDashboard.Core.Storage;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.Handy.Tools;

namespace TilesDashboard.Core.Domain.Services
{
    public class MetricService : IMetricService
    {
        private readonly ITileContext _context;

        private readonly IDateTimeOffsetProvider _dateTimeOffsetProvider;

        public MetricService(ITileContext context, IDateTimeOffsetProvider dateTimeOffsetProvider)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dateTimeOffsetProvider = dateTimeOffsetProvider ?? throw new ArgumentNullException(nameof(dateTimeOffsetProvider));
        }

        public async Task<MetricData> GetMetricRecentDataAsync(string tileName, CancellationToken token)
        {
            var tileDbEntity = await _context.GetTiles<MetricData>().Find(Filter(tileName)).SingleOrDefaultAsync(token);
            if (tileDbEntity.NotExists())
            {
                throw new NotFoundException($"Tile {tileName} does not exist.");
            }

            return tileDbEntity.Data.OrderBy(x => x.AddedOn).Last();
        }

        public async Task RecordMetricDataAsync(string tileName, MetricType metricType, decimal currentValue, CancellationToken token)
        {
            var metricData = new MetricData(currentValue, metricType, _dateTimeOffsetProvider.Now);
            var filter = Filter(tileName);

            var tile = await _context.GetTiles<MetricData>().Find(filter).SingleOrDefaultAsync(token);
            if (tile.NotExists())
            {
                throw new NotFoundException($"Tile {tileName} does not exist.");
            }

            var metricConfiguration = BsonSerializer.Deserialize<MetricConfiguration>(tile.Configuration);
            if (metricConfiguration.MetricType != metricType)
            {
                throw new NotSupportedOperationException($"Metric {tileName} is type of {metricConfiguration.MetricType} but type {metricType} has been passed.");
            }

            await _context.GetTiles<MetricData>().UpdateOneAsync(
               filter,
               Builders<TileDbEntity<MetricData>>.Update.Push(x => x.Data, metricData),
               null,
               token);
        }

        private FilterDefinition<TileDbEntity<MetricData>> Filter(string tileName)
        {
            return Builders<TileDbEntity<MetricData>>.Filter.And(
                    Builders<TileDbEntity<MetricData>>.Filter.Eq(x => x.Id.Name, tileName),
                    Builders<TileDbEntity<MetricData>>.Filter.Eq(x => x.Id.TileType, TileType.Metric));
        }
    }
}
