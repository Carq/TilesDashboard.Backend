using Microsoft.Extensions.Configuration;

namespace MetricsDashboard.WebApi.Configuration
{
    public class Settings : BaseSettings
    {
        public Settings(IConfiguration configuration)
            : base(configuration)
        {
        }

        public string ConnectionString => GetValue<string>("Application:ConnectionString");
    }
}
