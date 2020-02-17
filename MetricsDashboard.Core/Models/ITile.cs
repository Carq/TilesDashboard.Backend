using MetricsDashboard.Contract;

namespace MetricsDashboard.Core.Models
{
    public interface ITile
    {
        TileDto ToDto();
    }
}