using System;
using System.Threading.Tasks;
using TilesDashboard.Handy.Tools;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Integer;
using TilesDashboard.V2.Core.Repositories;

namespace TilesDashboard.V2.Core.Services
{
    public class IntegerService : TileBaseService, IIntegerService
    {
        public IntegerService(ITileRepository tileRepository, IDateTimeProvider dateTimeProvider)
            : base(tileRepository, dateTimeProvider)
        {
        }

        public async Task RecordValue(TileId tileId, int integerValue, DateTimeOffset? occurredOn)
        {
            await TileRepository.RecordValue(tileId, new IntegerValue(integerValue, occurredOn ?? DateTimeProvider.Now));
        }

        public async Task RecordValue(StorageId tileStorageId, int integerValue, DateTimeOffset? occurredOn)
        {
            await TileRepository.RecordValue(tileStorageId, new IntegerValue(integerValue, occurredOn ?? DateTimeProvider.Now), TileType.Integer);
        }
    }
}
