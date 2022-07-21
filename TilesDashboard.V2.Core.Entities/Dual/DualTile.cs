using TilesDashboard.Handy.Extensions;

namespace TilesDashboard.V2.Core.Entities.Dual
{
    public class DualTile : TileEntity
    {
        private DualConfiguration _dualConfiguration;

        public override object GetConfigurationAsObject() => GetDualConfiguration();

        public DualConfiguration GetDualConfiguration()
        {
            if (_dualConfiguration.NotExists())
            {
                _dualConfiguration = new DualConfiguration(TileConfiguration);
            }

            return _dualConfiguration;
        }
    }
}
