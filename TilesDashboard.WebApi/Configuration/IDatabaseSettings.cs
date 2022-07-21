namespace TilesDashboard.WebApi.Configuration
{
    public interface IDatabaseConfiguration
    {
        string ConnectionString { get; }

        string DatabaseName { get; }
    }
}
