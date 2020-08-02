using System;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Plugin.Crypto;
using TilesDashboard.Plugins.Terminal.Helpers;

namespace TilesDashboard.Plugins.Terminal
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Testing plugin...");

            var plugin = new LtcPlugin(new FakePluginConfigProvider());
            await plugin.InitializeAsync();
            var result = await plugin.GetDataAsync(CancellationToken.None);

            Console.WriteLine(result);

            Console.ReadKey();
        }
    }
}
