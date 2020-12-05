using System.Collections.Generic;
using System.Linq;
using TilesDashboard.Contract;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.WebApi.Mappers
{
    public static class ContractMapper
    {
        public static IList<TileWithCurrentDataDto> MapToContract(this IList<TileEntity> tileEntities)
        {
            return tileEntities.Select(MapToContract).ToList();
        }

        public static TileWithCurrentDataDto MapToContract(this TileEntity tileEntity)
        {
            return new TileWithCurrentDataDto
            {
                Name = tileEntity.TileId.Name,
                Type = tileEntity.TileId.Type,
                Configuration = tileEntity.GetConfigurationAsObject(),
            };
        }
    }
}
