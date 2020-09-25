namespace TilesDashboard.WebApi.Configuration
{
    public interface ISecurityConfig
    {
        string SecurityToken { get; }

        string SecretReadEndpoints { get; }

        bool ProtectReadEndpoints { get; }
    }
}
