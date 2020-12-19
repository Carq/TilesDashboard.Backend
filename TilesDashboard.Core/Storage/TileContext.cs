using MongoDB.Driver;
using TilesDashboard.Core.Storage.Entities;

namespace TilesDashboard.Core.Storage
{
    public class TileContext : ITileContext
    {
        private readonly IMongoDatabase _database;

        public TileContext(IMongoDatabase database)
        {
            _database = database ?? throw new System.ArgumentNullException(nameof(database));
        }

        public IMongoCollection<TileDbEntity> GetTiles() => _database.GetCollection<TileDbEntity>(CollectionNames.Tiles);
    }
}