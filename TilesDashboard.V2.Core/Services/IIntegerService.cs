using System.Threading.Tasks;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.V2.Core.Services
{
    public interface IIntegerService
    {
        Task RecordValue(TileId tileId, int integerValue);

        Task RecordValue(StorageId tileStorageId, int integerValue);
    }
}
