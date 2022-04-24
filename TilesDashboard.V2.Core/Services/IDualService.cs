using System;
using System.Threading.Tasks;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.V2.Core.Services
{
    public interface IDualService
    {
        Task RecordValue(TileId tileId, decimal primary, decimal secondary, DateTimeOffset? occurredOn);

        Task RecordValue(StorageId tileStorageId, decimal primary, decimal secondary, DateTimeOffset? occurredOn);
    }
}