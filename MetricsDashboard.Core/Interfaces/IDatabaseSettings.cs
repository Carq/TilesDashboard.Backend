namespace MetricsDashboard.Core.Interfaces
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; }

        string DatabaseName { get; }
    }
}