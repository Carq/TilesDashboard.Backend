using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Handy.Extensions;

namespace TilesDashboard.PluginSystem
{
    public class PluginConfigRepository : IPluginConfigRepository
    {
        public Task<IList<PluginConfigForTile>> GetAllPluginsConfiguration(CancellationToken cancellationToken)
        {
            return Task.FromResult(new PluginConfigForTile(
                        "TileCorePlugins.OpenWeatherMapPlugin",
                        new V2.Core.Entities.StorageId("5fb005a22c88cf3bbcae30b1"),
                        "40 36 20 * * 1,2,3,4,5",
                        new Dictionary<string, string>()
                        {
                            { "CityId", "3099230" },
                            { "ApiKey", "" }
                        }).ToOneElementList());
        }
    }
}
