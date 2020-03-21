using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Contract;

namespace TilesDashboard.Core.Services
{
    public interface ITileService
    {
        Task<IList<TileDto>> GetAllTilesAsync(CancellationToken cancellationToken);
    }
}