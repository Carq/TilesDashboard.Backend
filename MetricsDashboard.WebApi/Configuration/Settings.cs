using MetricsDashboard.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MetricsDashboard.WebApi.Configuration
{
    public class Settings : BaseSettings, IDatabaseSettings
    {
        public Settings(IConfiguration configuration)
            : base(configuration)
        {
        }

        public string ConnectionString => GetValue<string>("Application:ConnectionString");

        public string DatabaseName => GetValue<string>("Application:DatabaseName");
    }
}
