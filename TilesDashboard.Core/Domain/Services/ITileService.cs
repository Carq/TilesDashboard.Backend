using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Core.Domain.Entities;
using TilesDashboard.Core.Type;

namespace TilesDashboard.Core.Domain.Services
{
    public interface ITileService
    {
        Task<IList<GenericTileWithCurrentData>> GetAllAsync(int amountOfData, CancellationToken cancellationToken);

        Task SetGroupToTile(string tileName, TileType tileType, string groupName, CancellationToken cancellationToken);
    }
}
