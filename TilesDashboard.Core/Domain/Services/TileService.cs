using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TilesDashboard.Core.Domain.Entities;
using TilesDashboard.Core.Domain.Enums;
using TilesDashboard.Core.Entities;
using TilesDashboard.Core.Storage;
using TilesDashboard.Core.Storage.Entities;

namespace TilesDashboard.Core.Domain.Services
{
    public class TileService : ITileService
    {
        private readonly ITileContext _context;

        public TileService(ITileContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IList<GenericTileWithCurrentData>> GetAllAsync(CancellationToken token)
        {
            var weatherTiles = await _context.GetTiles().Find(_ => true).ToListAsync(token);

            var tilesWithCurrentData = new List<GenericTileWithCurrentData>();

            foreach (var tile in weatherTiles)
            {
                object configuration = null;
                TileData data = null;
                if (tile.Id.TileType == TileType.Metric)
                {
                    configuration = BsonSerializer.Deserialize<MetricConfiguration>(tile.Configuration);
                    data = BsonSerializer.Deserialize<MetricData>(tile.Data.Last());
                }
                else if (tile.Id.TileType == TileType.Weather)
                {
                    data = BsonSerializer.Deserialize<WeatherData>(tile.Data.Last());
                }

                tilesWithCurrentData.Add(new GenericTileWithCurrentData(tile.Id.Name, tile.Id.TileType, data, configuration));
            }

            return tilesWithCurrentData;
        }
    }
}
