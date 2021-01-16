using System.Threading.Tasks;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.V2.Core.Services
{
    public interface IHeartBeatService
    {
        Task RecordValue(TileId tileId, int responseInMs, string appVersion, string additionalInfo);

        Task RecordValue(StorageId tileStorageId, int responseInMs, string appVersion, string additionalInfo);
    }
}
