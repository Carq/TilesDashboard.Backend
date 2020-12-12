using System.Threading.Tasks;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.V2.Core.Services
{
    public interface IWeatherService
    {
        Task RecordValue(TileId tileId, decimal temperature, decimal humidity);

        Task RecordValue(TileStorageId tileStorageId, decimal temperature, decimal humidity);
    }
}
