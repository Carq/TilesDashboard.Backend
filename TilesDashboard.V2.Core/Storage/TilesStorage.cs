using MongoDB.Driver;
using TilesDashboard.V2.Core.Configuration;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.V2.Core.Storage
{
    public class TilesStorage : ITilesStorage
    {
        private readonly IMongoDatabase _database;

        public TilesStorage(IDatabaseConfiguration config)
        {
            var client = new MongoClient(config.ConnectionString);
            _database = client.GetDatabase(config.DatabaseName);
        }

        public IMongoCollection<TileEntity> TilesInformation => _database.GetCollection<TileEntity>(CollectionNames.TilesInformation);

        public IMongoCollection<TileData> TilesData => _database.GetCollection<TileData>(CollectionNames.TilesData);
    }
}
