using System.Threading.Tasks;
using TilesDashboard.PluginBase.WeatherPluginBase;

namespace TilesDashboard.PluginBase
{
    public interface IWeatherPlugin
    {
        public string TileName { get; }

        Task<WeatherData> GetDataAsync();
    }
}
