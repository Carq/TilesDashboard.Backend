using TilesDashboard.PluginBase.Data;

namespace TilesDashboard.WebApi.PluginSystem.Extensions
{
    public static class StatusExtensions
    {
        public static bool IsError(this Status status)
        {
            return ((int)status) >= 10;
        }
    }
}
