using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.PluginBase
{
    public interface ITileDataProvider<TTileData> 
        where TTileData : ITileValue
    {
        Task<IList<TTileData>> GetLast5Data(CancellationToken cancellationToken);
    }
}
