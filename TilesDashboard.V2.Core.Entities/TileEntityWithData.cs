using System.Collections.Generic;
using TilesDashboard.Handy.Extensions;

namespace TilesDashboard.V2.Core.Entities
{
    public class TileEntityWithData
    {
        public TileEntityWithData(TileEntity tileEntity)
        {
            TileEntity = tileEntity ?? throw new System.ArgumentNullException(nameof(tileEntity));
        }

        public TileEntity TileEntity { get; }

        public IList<TileValue> Data { get; } = new List<TileValue>();

        public Group Group => TileEntity.Group;

        public void AddData(IList<TileValue> data)
        {
            Data.AddRange(data);
        }
    }
}
