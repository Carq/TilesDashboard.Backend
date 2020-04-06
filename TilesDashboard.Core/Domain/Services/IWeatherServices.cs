using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Core.Domain.Entities;
using TilesDashboard.Core.Domain.ValueObjects;

namespace TilesDashboard.Core.Domain.Services
{
    public interface IWeatherServices
    {
        Task<IList<WeatherData>> GetWeatherRecentDataAsync(string tileName, int amountOfData, CancellationToken token);

        Task RecordWeatherDataAsync(string tileName, Temperature temperature, Percentage huminidy, DateTimeOffset? dateOfChange, CancellationToken token);
    }
}
