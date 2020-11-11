using System.Threading;

namespace TilesDashboard.Handy.Tools
{
    public interface ICancellationTokenProvider
    {
        CancellationToken GetToken();
    }
}