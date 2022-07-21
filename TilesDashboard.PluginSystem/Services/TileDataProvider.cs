using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.PluginBase;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Repositories;

namespace TilesDashboard.PluginSystem.Services
{
    public class TileDataProvider<TTileData> : ITileDataProvider<TTileData>
        where TTileData : ITileValue
    {
        private readonly TileId _tileId;

        private readonly ITileRepository _tileRepository;

        private IList<TTileData> _tileData;

        public TileDataProvider(TileId tileId, ITileRepository tileRepository)
        {
            _tileId = tileId;
            _tileRepository = tileRepository;
        }

        public async Task<IList<TTileData>> GetLast5Data(CancellationToken cancellationToken)
        {
            if (_tileData == null)
            {
                var tileData = await _tileRepository.GetRecentTileValues(_tileId, 5);
                _tileData = tileData.Cast<TTileData>().ToList();
            }
            
            return _tileData;
        }
    }
}
