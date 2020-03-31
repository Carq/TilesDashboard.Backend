using Microsoft.Extensions.Configuration;
using TilesDashboard.Core.Configuration;

namespace TilesDashboard.WebApi.Configuration
{
    public class TileDashboardSettings : BaseSettings, IDatabaseConfiguration
    {
        public TileDashboardSettings(IConfiguration configuration)
            : base(configuration)
        {
        }

        public string ConnectionString => GetValue<string>("Application:ConnectionString");

        public string DatabaseName => GetValue<string>("Application:DatabaseName");

        public string MongoConnectionString => GetValue<string>("Application:MongoConnectionString");
    }
}
