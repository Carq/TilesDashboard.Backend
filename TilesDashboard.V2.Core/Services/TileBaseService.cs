using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Repositories;

namespace TilesDashboard.V2.Core.Services
{
    public class TileBaseService : ITileBaseService
    {
        public TileBaseService(ITileRepository tileRepository)
        {
            TileRepository = tileRepository ?? throw new ArgumentNullException(nameof(tileRepository));
        }

        protected ITileRepository TileRepository { get; }

        public async Task<IList<TileEntity>> GetAllTiles()
        {
            return await TileRepository.GetAll();
        }

        public async Task<TTile> GetTile<TTile>(TileId tileId)
            where TTile : TileEntity
        {
            return await TileRepository.GetTile<TTile>(tileId);
        }

        public async Task<TileEntity> GetTile(TileId tileId)
        {
            return await TileRepository.GetTile<TileEntity>(tileId);
        }
    }
}
