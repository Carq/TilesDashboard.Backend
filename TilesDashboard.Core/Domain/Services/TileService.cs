using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TilesDashboard.Core.Domain.Entities;
using TilesDashboard.Core.Entities;
using TilesDashboard.Core.Storage;

namespace TilesDashboard.Core.Domain.Services
{
    public class TileService : ITileService
    {
        private readonly ITileContext _context;

        public TileService(ITileContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IList<TileWithCurrentData>> GetAllAsync(CancellationToken token)
        {
            var weatherTiles = await _context.GetTiles<WeatherData>().Find(Filters.WeatherTiles).ToListAsync(token);
            var metricTiles = await _context.GetTiles<MetricData>().Find(Filters.MetricsTiles).ToListAsync(token);

            var tilesWithCurrentData = new List<TileWithCurrentData>();

            foreach (var tile in weatherTiles)
            {
                tilesWithCurrentData.Add(new TileWithCurrentData(tile.Id.Name, tile.Id.TileType, tile.Data.Last(), tile.Configuration));
            }

            foreach (var tile in metricTiles)
            {
                tilesWithCurrentData.Add(new TileWithCurrentData(tile.Id.Name, tile.Id.TileType, tile.Data.Last(), BsonSerializer.Deserialize<MetricConfiguration>(tile.Configuration)));
            }

            return tilesWithCurrentData;
        }
    }
}
