namespace TilesDashboard.V2.Core.Configuration
{
    public interface IDatabaseConfiguration
    {
        string ConnectionString { get; }

        string DatabaseName { get; }
    }
}
