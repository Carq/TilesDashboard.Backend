using Dawn;
using TilesDashboard.Core.Domain.Enums;

namespace TilesDashboard.Core.Domain.Entities
{
    public class TileWithCurrentData
    {
        public TileWithCurrentData(string name, TileType type, object currentData, object configuration = null)
        {
            Name = Guard.Argument(name, nameof(name)).NotNull().NotEmpty();
            Type = Guard.Argument(type, nameof(type)).NotDefault();
            CurrentData = currentData;
            Configuration = configuration;
        }

        public string Name { get; private set; }

        public TileType Type { get; private set; }

        public object Configuration { get; private set; }

        public object CurrentData { get; private set; }
    }
}
