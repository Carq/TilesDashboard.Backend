using MongoDB.Driver;
using TilesDashboard.Core.Configuration;
using TilesDashboard.Core.Entities;
using TilesDashboard.Core.Storage.Constants;
using TilesDashboard.Core.Storage.Entities;

namespace TilesDashboard.Core.Storage
{
    public class TileContext : ITileContext
    {
        private readonly IMongoDatabase _database = null;

        public TileContext(IDatabaseConfiguration config)
        {
            var client = new MongoClient(config.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(config.DatabaseName);
            }
        }

        public IMongoCollection<TileDbEntity<TData>> GetTiles<TData>()
           where TData : TileData => _database.GetCollection<TileDbEntity<TData>>(CollectionNames.Tiles);
    }
}
