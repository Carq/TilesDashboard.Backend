using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Core.Domain.Entities;

namespace TilesDashboard.Core.Domain.Services
{
    public interface IWeatherServices
    {
        Task<WeatherData> GetWeatherRecentDataAsync(string tileName, CancellationToken token);

        Task RecordWeatherDataAsync(string tileName, decimal temperature, decimal huminidy, CancellationToken token);
    }
}
