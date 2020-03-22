using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TilesDashboard.Core.Domain.Services
{
    public interface ITileService
    {
        Task<IList<object>> GetAllAsync(CancellationToken cancellationToken);
    }
}
