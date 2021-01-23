using TilesDashboard.Handy.Extensions;

namespace TilesDashboard.V2.Core.Entities.Integer
{
    public class IntegerTile : TileEntity
    {
        private IntegerConfiguration _integerConfiguration;

        public override object GetConfigurationAsObject() => GetIntegerConfiguration();

        public IntegerConfiguration GetIntegerConfiguration()
        {
            if (_integerConfiguration.NotExists())
            {
                _integerConfiguration = new IntegerConfiguration(TileConfiguration);
            }

            return _integerConfiguration;
        }
    }
}
