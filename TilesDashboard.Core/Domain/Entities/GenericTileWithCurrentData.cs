using Dawn;
using TilesDashboard.Core.Domain.Enums;
using TilesDashboard.Core.Storage.Entities;

namespace TilesDashboard.Core.Domain.Entities
{
    public class GenericTileWithCurrentData
    {
        public GenericTileWithCurrentData(string name, TileType type, TileData currentData, object configuration = null)
        {
            Name = Guard.Argument(name, nameof(name)).NotNull().NotEmpty();
            Type = Guard.Argument(type, nameof(type)).NotDefault();
            CurrentData = currentData;
            Configuration = configuration;
        }

        public string Name { get; private set; }

        public TileType Type { get; private set; }

        public object Configuration { get; private set; }

        public TileData CurrentData { get; private set; }
    }
}
