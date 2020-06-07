using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dawn;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TilesDashboard.Core.Domain.Entities;
using TilesDashboard.Core.Domain.Enums;
using TilesDashboard.Core.Domain.Repositories;
using TilesDashboard.Core.Storage;
using TilesDashboard.Core.Storage.Entities;

namespace TilesDashboard.Core.Domain.Services
{
    public class TileService : ITileService
    {
        private readonly ITileContext _context;

        public TileService(ITileContext context, ITilesRepository tilesRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            TilesRepository = tilesRepository ?? throw new ArgumentNullException(nameof(tilesRepository));
        }

        protected ITilesRepository TilesRepository { get; }

        public async Task<IList<GenericTileWithCurrentData>> GetAllAsync(int amountOfData, CancellationToken cancellationToken)
        {
            Guard.Argument(amountOfData, nameof(amountOfData)).GreaterThan(0);
            var allTiles = await TilesRepository.GetAllTilesWithLimitedRecentData(amountOfData, cancellationToken);

            var tilesWithCurrentData = new List<GenericTileWithCurrentData>();

            foreach (var tile in allTiles)
            {
                object configuration = null;
                var rawData = tile.Data.TakeLast(amountOfData);
                var data = new List<TileData>();
                if (tile.Id.TileType == TileType.Metric)
                {
                    configuration = BsonSerializer.Deserialize<MetricConfiguration>(tile.Configuration);
                    data = DeserializeData<MetricData>(rawData).Cast<TileData>().ToList();
                }
                else if (tile.Id.TileType == TileType.Weather)
                {
                    data = DeserializeData<WeatherData>(rawData).Cast<TileData>().ToList();
                }

                tilesWithCurrentData.Add(new GenericTileWithCurrentData(tile.Id.Name, tile.Id.TileType, data, new Group(tile.Group), configuration));
            }

            return tilesWithCurrentData;
        }

        public async Task SetGroupToTile(string tileName, TileType tileType, string groupName, CancellationToken cancellationToken)
        {
            await _context.GetTiles().UpdateOneAsync(
                Filter(tileName),
                Builders<TileDbEntity>.Update.Set(x => x.Group, groupName),
                null,
                cancellationToken);
        }

        public async Task<IList<TData>> GetRecentDataAsync<TData>(string tileName, TileType type, int amountOfData, CancellationToken cancellationToken)
            where TData : TileData
        {
            var tileDbEntity = await TilesRepository.GetTileWithLimitedRecentData(tileName, type, amountOfData, cancellationToken);
            var rawWeatherData = tileDbEntity.Data.OrderBy(x => x[nameof(TileData.AddedOn)]).TakeLast(amountOfData);
            return DeserializeData<TData>(rawWeatherData);
        }

        protected static List<TData> DeserializeData<TData>(IEnumerable<BsonDocument> rawData)
            where TData : TileData
        {
            var result = new List<TData>();
            foreach (var item in rawData)
            {
                result.Add(BsonSerializer.Deserialize<TData>(item));
            }

            return result.OrderByDescending(x => x.AddedOn).ToList();
        }

        private FilterDefinition<TileDbEntity> Filter(string tileName)
        {
            return Builders<TileDbEntity>.Filter.And(
                Builders<TileDbEntity>.Filter.Eq(x => x.Id.Name, tileName),
                Builders<TileDbEntity>.Filter.Eq(x => x.Id.TileType, TileType.Weather));
        }
    }
}
