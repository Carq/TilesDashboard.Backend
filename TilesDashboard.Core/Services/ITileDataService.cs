using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Contract;

namespace TilesDashboard.Core.Services
{
    public interface ITileDataService
    {
        Task SaveMetricAsync(SaveValueDto<decimal> saveValueDto, CancellationToken cancellationToken);

        Task SaveStatusAsync(SaveValueDto<bool> saveValueDto, CancellationToken cancellationToken);
    }
}