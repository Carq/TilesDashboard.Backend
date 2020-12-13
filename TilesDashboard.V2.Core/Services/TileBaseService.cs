using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TilesDashboard.Handy.Tools;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Repositories;

namespace TilesDashboard.V2.Core.Services
{
    public class TileBaseService : ITileBaseService
    {
        private readonly int _amountOfRecentData = 5;

        public TileBaseService(ITileRepository tileRepository, IDateTimeProvider dateTimeProvider)
        {
            TileRepository = tileRepository ?? throw new ArgumentNullException(nameof(tileRepository));
            DateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        protected IDateTimeProvider DateTimeProvider { get; }

        protected ITileRepository TileRepository { get; }

        public async Task<IList<TileEntity>> GetAllTiles()
        {
            return await TileRepository.GetAll();
        }

        public async Task<IList<TileEntityWithData>> GetAllTilesWithRecentData()
        {
            var allTiles = await TileRepository.GetAll();
            var tilesWithRecentData = new List<TileEntityWithData>();
            foreach (var tile in allTiles)
            {
                var tileWithData = new TileEntityWithData(tile);
                tileWithData.AddData(await TileRepository.GetRecentTileValues(tile.TileId, _amountOfRecentData));
                tilesWithRecentData.Add(tileWithData);
            }

            return tilesWithRecentData.OrderBy(x => x.TileEntity.TileId.Name).ToList();
        }

        public async Task<TileEntity> GetTile(TileId tileId)
        {
            return await TileRepository.GetTile<TileEntity>(tileId);
        }

        public async Task<IList<TileValue>> GetTileRecentData(TileId tileId, int amountOfRecentData)
        {
            return await TileRepository.GetRecentTileValues(tileId, amountOfRecentData);
        }

        public async Task<IList<TileValue>> GetTileSinceData(TileId tileId, int hours)
        {
            return await TileRepository.GetTileValuesSince(tileId, DateTimeProvider.Now.AddHours(-hours));
        }
    }
}
