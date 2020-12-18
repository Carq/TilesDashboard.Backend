using System.Threading;
using TilesDashboard.Handy.Tools;

namespace TilesDashboard.PluginSystem
{
    public class CancelletionTokenProvider : ICancellationTokenProvider
    {
        public CancellationToken GetToken()
        {
            return CancellationToken.None;
        }
    }
}
