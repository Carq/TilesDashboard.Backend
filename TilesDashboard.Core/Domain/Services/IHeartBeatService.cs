using System.Threading;
using System.Threading.Tasks;

namespace TilesDashboard.Core.Domain.Services
{
    public interface IHeartBeatService
    {
        Task RecordDataAsync(string tileName, int responseInMs, string appVersion, CancellationToken cancellationToken);
    }
}
