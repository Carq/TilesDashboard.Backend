namespace MetricsDashboard.DataAccess.Interfaces
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; }

        string DatabaseName { get; }
    }
}