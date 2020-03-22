using MongoDB.Driver;
using TilesDashboard.Core.Domain.Entities;
using TilesDashboard.Core.Domain.Enums;
using TilesDashboard.Core.Entities;

namespace TilesDashboard.Core.Domain.Services
{
    public static class Filters
    {
        public static readonly FilterDefinition<TileDbEntity<WeatherData>> WeatherTiles = Builders<TileDbEntity<WeatherData>>.Filter.Eq(x => x.Id.TileType, TileType.Weather);

        public static readonly FilterDefinition<TileDbEntity<MetricData>> MetricsTiles = Builders<TileDbEntity<MetricData>>.Filter.Eq(x => x.Id.TileType, TileType.Metric);
    }
}
