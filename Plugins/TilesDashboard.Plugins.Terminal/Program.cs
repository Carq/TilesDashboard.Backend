using System;
using System.Threading.Tasks;
using TilesDashboard.Plugin.OpenWeatherMap;
using TilesDashboard.Plugins.Terminal.Helpers;

namespace TilesDashboard.Plugins.Terminal
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var plugin = new OpenWeatherMapPlugin(new FakePluginConfigProvider());
            var result = await plugin.GetDataAsync();

            Console.ReadKey();
        }
    }
}
