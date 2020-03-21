using Dawn;
using TilesDashboard.Core.Domain.Enums;

namespace TilesDashboard.Core.Domain.Entities
{
    public abstract class TileBase
    {
        protected TileBase(string name, TileType type)
        {
            Name = Guard.Argument(name, nameof(name)).NotNull().NotEmpty();
            Type = Guard.Argument(type, nameof(type)).NotDefault();
        }

        public string Name { get; private set; }

        public TileType Type { get; private set; }
    }
}
