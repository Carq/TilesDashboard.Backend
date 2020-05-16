using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace TilesDashboard.WebApi.Configuration
{
    [SuppressMessage("Microsoft.Performance", "CA1819", Justification = "Allowed here")]
    public class CorsConfig : BaseSettings
    {
        public CorsConfig(IConfiguration configuration) : base(configuration)
        {
        }

        public string BaseRoot { get; } = "Cors:";

        public string Name => GetValue<string>(BaseRoot + nameof(Name));

        public string[] AllowedHeaders => GetArray(BaseRoot + nameof(AllowedHeaders));

        public string[] AllowedOrigins => GetArray(BaseRoot + nameof(AllowedOrigins));

        public string[] AllowedMethods => GetArray(BaseRoot + nameof(AllowedMethods));

        public string[] ExposedHeaders => GetArray(BaseRoot + nameof(ExposedHeaders));

        public bool AllowCredentials => GetValue<bool>(BaseRoot + nameof(AllowCredentials));
    }
}
