namespace TilesDashboard.Core.Configuration
{
    public interface IDatabaseConfiguration
    {
        string ConnectionString { get; }

        string DatabaseName { get; }
    }
}
