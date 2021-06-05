using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TilesDashboard.PluginSystem.DataPlugins;
using TilesDashboard.WebApi.PluginSystem.Loaders;

namespace TilesDashboard.WebApi.StartupConfig
{
    public static class LoadDataPluginsStartup
    {
        public static void LoadDataPlugins(this IApplicationBuilder applicationBuilder)
        {
            var dataPluginContext = applicationBuilder.ApplicationServices.GetService<IDataPluginContext>();
            var dataPluginLoader = applicationBuilder.ApplicationServices.GetService<IDataPluginLoader>();

            var dataPlugins = dataPluginLoader?.LoadDataPluginsAsync(AppDomain.CurrentDomain.BaseDirectory).Result;
            dataPluginContext?.AddPlugins(dataPlugins);
        }
    }
}
