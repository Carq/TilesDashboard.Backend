﻿using System.Threading.Tasks;
using TilesDashboard.Handy.Tools;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Dual;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Repositories;

namespace TilesDashboard.V2.Core.Services
{
    public class DualService : TileBaseService, IDualService
    {
        public DualService(IDateTimeProvider dateTimeProvider, ITileRepository tileRepository)
            : base(tileRepository, dateTimeProvider)
        {
        }

        public async Task<DualTile> GetWeatherTile(TileId tileId) => await TileRepository.GetTile<DualTile>(tileId);

        public async Task RecordValue(TileId tileId, decimal primary, decimal secondary)
        {
            var dualValue = new DualValue(primary, secondary, DateTimeProvider.Now);
            await TileRepository.RecordValue(tileId, dualValue);
        }

        public async Task RecordValue(StorageId tileStorageId, decimal primary, decimal secondary)
        {
            var dualValue = new DualValue(primary, secondary, DateTimeProvider.Now);
            await TileRepository.RecordValue(tileStorageId, dualValue, TileType.Dual);
        }
    }
}
