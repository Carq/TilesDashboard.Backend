using System.Threading.Tasks;
using TilesDashboard.Handy.Tools;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.HeartBeat;
using TilesDashboard.V2.Core.Repositories;

namespace TilesDashboard.V2.Core.Services
{
    public class HeartBeatService : TileBaseService, IHeartBeatService
    {
        public HeartBeatService(ITileRepository tileRepository, IDateTimeProvider dateTimeProvider)
            : base(tileRepository, dateTimeProvider)
        {
        }

        public async Task RecordValue(TileId tileId, int responseInMs, string appVersion, string additionalInfo)
        {
            await TileRepository.RecordValue(tileId, new HeartBeatValue(responseInMs, appVersion, additionalInfo, DateTimeProvider.Now));
        }

        public async Task RecordValue(StorageId tileStorageId, int responseInMs, string appVersion, string additionalInfo)
        {
            await TileRepository.RecordValue(tileStorageId, new HeartBeatValue(responseInMs, appVersion, additionalInfo, DateTimeProvider.Now), TileType.HeartBeat);
        }
    }
}
