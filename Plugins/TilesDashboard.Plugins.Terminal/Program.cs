using System;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Plugin.Azure.CodeCoverage;
using TilesDashboard.Plugin.OpenWeatherMap;
using TilesDashboard.Plugins.Terminal.Helpers;

namespace TilesDashboard.Plugins.Terminal
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var plugin = new AzureCodeCoveragePlugin(new FakePluginConfigProvider());
            await plugin.InitializeAsync();
            var result = await plugin.GetDataAsync(CancellationToken.None);

            Console.ReadKey();
        }
    }
}
