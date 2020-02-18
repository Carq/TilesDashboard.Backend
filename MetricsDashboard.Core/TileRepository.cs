using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.Contract.Enums;
using MetricsDashboard.Core.Constants;
using MetricsDashboard.Core.Entities;
using MetricsDashboard.Core.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MetricsDashboard.Core
{
    public class TileRepository : ITileRepository
    {
        private readonly IMongoDatabase _database;

        private readonly IMongoCollection<TileEntity> _tiles;

        private readonly IMongoCollection<MetricSettingsEntity> _metricSettings;

        public TileRepository(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.MongoConnectionString);
            _database = client.GetDatabase(databaseSettings.DatabaseName);

            _tiles = _database.GetCollection<TileEntity>(CollectionNames.Tiles);
            _metricSettings = _database.GetCollection<MetricSettingsEntity>(CollectionNames.MetricSettings);
        }

        public async Task<TileEntity> GetTileAsync(string tileName, CancellationToken cancellationToken)
        {
            return await _tiles.AsQueryable().FirstOrDefaultAsync(t => t.Name == tileName, cancellationToken);
        }

        [SuppressMessage(
            "Microsoft.Maintainability",
            "CA1506: Avoid excessive class coupling",
            Justification = "Needs to be investigated how to do it better (with less coupling).")]
        public async Task<IList<ITile>> GetMetricTilesAsync(CancellationToken cancellationToken)
        {
            var query = from tile in _tiles.AsQueryable().Where(t => t.TileType == TileType.Metric)
                join setting in _metricSettings.AsQueryable() on tile.Id equals setting.TileId
                join metric in GetMetrics<decimal>().AsQueryable() on tile.Id equals metric.TileId into metrics
                select new { tile.Id, tile.Name, setting,  metrics };

            var result = await query.ToListAsync(cancellationToken);
            return result.Select(x => new MetricTile(x.Name, x.setting, x.metrics)).ToList<ITile>();
        }

        public async Task<IList<ITile>> GetBooleanTilesAsync(CancellationToken cancellationToken)
        {
            var query = from tile in _tiles.AsQueryable().Where(t => t.TileType == TileType.Boolean)
                join metric in GetMetrics<bool>().AsQueryable() on tile.Id equals metric.TileId into metrics
                select new { tile.Name, metrics };

            var result = await query.ToListAsync(cancellationToken);
            return result.Select(x => new BooleanTile(x.Name, x.metrics)).ToList<ITile>();
        }

        public async Task SaveTileDataAsync<TValue>(MetricEntity<TValue> metric, CancellationToken cancellationToken)
        {
            await GetMetrics<TValue>().InsertOneAsync(metric, new InsertOneOptions(), cancellationToken);
        }

        private IMongoCollection<MetricEntity<TValue>> GetMetrics<TValue>() =>
            _database.GetCollection<MetricEntity<TValue>>(CollectionNames.Metrics);
    }
}
