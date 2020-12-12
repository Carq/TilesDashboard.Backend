using System.Collections.Generic;
using System.Linq;
using TilesDashboard.Contract;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.WebApi.Mappers
{
    public static class ContractMapper
    {
        public static IList<object> MapToContract(this IList<TileValue> tileValues)
        {
            return tileValues.Cast<object>().ToList();
        }

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
                Group = new GroupDto(string.Empty, 0),
            };
        }

        public static IList<TileWithCurrentDataDto> MapToContract(this IList<TileEntityWithData> tilesWithData)
        {
            return tilesWithData.Select(MapToContract).ToList();
        }

        public static TileWithCurrentDataDto MapToContract(this TileEntityWithData tileEntityWithData)
        {
            var mappedTileWithData = new TileWithCurrentDataDto
            {
                Name = tileEntityWithData.TileEntity.TileId.Name,
                Type = tileEntityWithData.TileEntity.TileId.Type,
                Configuration = tileEntityWithData.TileEntity?.GetConfigurationAsObject(),
                Group = new GroupDto(string.Empty, 0),
            };

            mappedTileWithData.Data.AddRange(tileEntityWithData.Data.Cast<object>().ToList());
            return mappedTileWithData;
        }
    }
}
