using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.Contract;

namespace MetricsDashboard.Core
{
    public interface ITileDataService
    {
        Task SaveMetricAsync(SaveValueDto<decimal> saveValueDto, CancellationToken cancellationToken);

        Task SaveBooleanAsync(SaveValueDto<bool> saveValueDto, CancellationToken cancellationToken);
    }
}