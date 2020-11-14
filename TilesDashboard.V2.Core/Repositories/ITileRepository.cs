using System.Threading.Tasks;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Metric;

namespace TilesDashboard.V2.Core.Repositories
{
    public interface ITileRepository
    {
        Task<TEntity> GetTile<TEntity>(TileId tileId) 
            where TEntity : TileEntity;

        void RecordValue(TileId tileId, MetricValue newMetricValue);
    }
}