using System;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Core.Domain.Entities;
using TilesDashboard.Core.Domain.ValueObjects;

namespace TilesDashboard.Core.Domain.Services
{
    public interface IWeatherServices
    {
        Task<WeatherData> GetWeatherRecentDataAsync(string tileName, CancellationToken token);

        Task RecordWeatherDataAsync(string tileName, Temperature temperature, Percentage huminidy, DateTimeOffset? dateOfChange, CancellationToken token);
    }
}
