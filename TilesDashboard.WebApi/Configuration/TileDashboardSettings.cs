﻿using Microsoft.Extensions.Configuration;
using TilesDashboard.V2.Core.Configuration;

namespace TilesDashboard.WebApi.Configuration
{
    public class TileDashboardSettings : BaseSettings, IDatabaseConfiguration, ISecurityConfig
    {
        public TileDashboardSettings(IConfiguration configuration)
            : base(configuration)
        {
        }

        public string ConnectionString => GetValue<string>("Application:ConnectionString");

        public string DatabaseName => GetValue<string>("Application:DatabaseName");

        public string SecurityToken => GetValue<string>("Application:SecurityToken");

        public string SecretReadEndpoints => GetValueOrDefault<string>("Application:SecretReadEndpoints");

        public bool ProtectReadEndpoints => GetValueOrDefault<bool>("Application:ProtectReadEndpoints");
    }
}
