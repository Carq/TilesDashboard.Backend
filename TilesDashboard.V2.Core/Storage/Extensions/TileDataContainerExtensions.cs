using MongoDB.Driver;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.V2.Core.Storage.Extensions
{
    public static class TileDataContainerExtensions
    {
        public static ProjectionDefinition<TileDataContainer> FetchRecentData(
            this ProjectionDefinitionBuilder<TileDataContainer> source, int amountOfData)
        {
            return source.Slice(x => x.Data, -amountOfData);
        }

        public static FilterDefinition<TileDataContainer> TileEntityFilter(StorageId tileStorageId, TileType tileType)
        {
            return Builders<TileDataContainer>.Filter.And(
                  Builders<TileDataContainer>.Filter.Eq(x => x.TileStorageId, tileStorageId.Value),
                  Builders<TileDataContainer>.Filter.Eq(x => x.Type, tileType));
        }
    }
}