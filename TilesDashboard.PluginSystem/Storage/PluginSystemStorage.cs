using MongoDB.Driver;
using System;
using TilesDashboard.PluginSystem.Entities;

namespace TilesDashboard.PluginSystem.Storage
{
    public class PluginSystemStorage : IPluginSystemStorage
    {
        private readonly IMongoDatabase _database;

        public PluginSystemStorage(IMongoDatabase database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

         public IMongoCollection<PluginConfiguration> PluginsConfigurations => _database.GetCollection<PluginConfiguration>(nameof(PluginsConfigurations));
    }
}
