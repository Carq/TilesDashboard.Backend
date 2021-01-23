using TilesDashboard.Handy.Extensions;

namespace TilesDashboard.V2.Core.Entities.HeartBeat
{
    public class HeartBeatTile : TileEntity
    {
        private HeartBeatConfiguration _heartBeatConfiguration;

        public override object GetConfigurationAsObject() => GetHeartBeatConfiguration();

        public HeartBeatConfiguration GetHeartBeatConfiguration()
        {
            if (_heartBeatConfiguration.NotExists())
            {
                _heartBeatConfiguration = new HeartBeatConfiguration(TileConfiguration);
            }

            return _heartBeatConfiguration;
        }
    }
}
