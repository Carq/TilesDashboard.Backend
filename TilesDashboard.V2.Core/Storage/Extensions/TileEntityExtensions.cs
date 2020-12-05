using MongoDB.Driver;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.Core.Domain.Extensions
{
    public static class TileEntityExtensions
    {
        public static FilterDefinition<TileEntity> TileEntityFilter(TileId tileId)
        {
            return Builders<TileEntity>.Filter.And(
                Builders<TileEntity>.Filter.Eq(x => x.TileId.Name, tileId.Name),
                Builders<TileEntity>.Filter.Eq(x => x.TileId.Type, tileId.Type));
        }
    }
}