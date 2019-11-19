namespace MetricsDashboard.WebApi.Services
{
    public interface IMetricService
    {
        void SaveValue(int metricId, int value);
    }
}