using MetricsDashboard.Dto;

namespace MetricsDashboard.DataAccess.Entities
{
    public interface IMetric
    {
        Metric ToDto();
    }
}