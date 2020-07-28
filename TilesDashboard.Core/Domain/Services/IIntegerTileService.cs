using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Core.Domain.Entities;

namespace TilesDashboard.Core.Domain.Services
{
    public interface IIntegerTileService
    {
        Task<IList<IntegerData>> GetIntegerDataSinceAsync(string tileName, int sinceDays, CancellationToken token);

        Task<IList<IntegerData>> GetIntegerRecentDataAsync(string tileName, int amountOfData, CancellationToken token);

        Task RecordIntegerDataAsync(string tileName, int value, CancellationToken cancellationToken);
    }
}