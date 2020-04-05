using System.Collections.Generic;
using Dawn;
using TilesDashboard.Core.Domain.Enums;
using TilesDashboard.Core.Storage.Entities;
using TilesDashboard.Handy.Extensions;

namespace TilesDashboard.Core.Domain.Entities
{
    public class GenericTileWithCurrentData
    {
        public GenericTileWithCurrentData(string name, TileType type, TileData currentData, IList<TileData> recentData, object configuration = null)
        {
            Name = Guard.Argument(name, nameof(name)).NotNull().NotEmpty();
            Type = Guard.Argument(type, nameof(type)).NotDefault();
            CurrentData = currentData;
            RecentData.AddRange(recentData);
            Configuration = configuration;
        }

        public string Name { get; private set; }

        public TileType Type { get; private set; }

        public object Configuration { get; private set; }

        public TileData CurrentData { get; private set; }

        public IList<TileData> RecentData { get; private set; } = new List<TileData>();
    }
}
