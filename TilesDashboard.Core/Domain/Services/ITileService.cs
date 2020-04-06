using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Core.Domain.Entities;

namespace TilesDashboard.Core.Domain.Services
{
    public interface ITileService
    {
        Task<IList<GenericTileWithCurrentData>> GetAllAsync(int amountOfData, CancellationToken token);
    }
}
