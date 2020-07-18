using MongoDB.Driver;
using TilesDashboard.Core.Configuration;
using TilesDashboard.Core.Storage.Entities;

namespace TilesDashboard.Core.Storage
{
    public class TileContext : ITileContext
    {
        private readonly IMongoDatabase _database;

        public TileContext(IDatabaseConfiguration config)
        {
            var client = new MongoClient(config.ConnectionString);
            _database = client.GetDatabase(config.DatabaseName);
        }

        public IMongoCollection<TileDbEntity> GetTiles() => _database.GetCollection<TileDbEntity>(CollectionNames.Tiles);
    }
}