using MongoDB.Driver;
using TilesDashboard.PluginSystem.Entities;

namespace TilesDashboard.PluginSystem.Storage
{
    public interface IPluginSystemStorage
    {
        IMongoCollection<PluginConfiguration> PluginsConfigurations { get; }
    }
}
