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
using TilesDashboard.Core.Entities;
using TilesDashboard.Core.Exceptions;
using TilesDashboard.Core.Storage;
using TilesDashboard.Core.Storage.Entities;
using TilesDashboard.Handy.Extensions;

namespace TilesDashboard.Core.Domain.Services
{
    public class TileService : ITileService
    {
        private readonly ITileContext _context;

        public TileService(ITileContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IList<GenericTileWithCurrentData>> GetAllAsync(int amountOfData, CancellationToken token)
        {
            Guard.Argument(amountOfData, nameof(amountOfData)).GreaterThan(0);

            var allTiles = await _context.GetTiles().Find(_ => true).ToListAsync(token);
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

                tilesWithCurrentData.Add(new GenericTileWithCurrentData(tile.Id.Name, tile.Id.TileType, data, configuration));
            }

            return tilesWithCurrentData;
        }

        public async Task<IList<TData>> GetRecentDataAsync<TData>(string tileName, TileType type, int amountOfData, CancellationToken token)
            where TData : TileData
        {
            var tileDbEntity = await _context.GetTiles().Find(x => x.Id.Name.ToLowerInvariant() == tileName.ToLowerInvariant() && x.Id.TileType == type).SingleOrDefaultAsync(token);
            if (tileDbEntity.NotExists())
            {
                throw new NotFoundException($"Tile {tileName} with Type {type} does not exist.");
            }

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
    }
}
