using MongoDB.Driver;
using TilesDashboard.Core.Storage.Entities;
using TilesDashboard.V2.Core.Configuration;

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