namespace TilesDashboard.Contract.RecordData
{
    public class RecordHeartBeatValueDto
    {
        public int ResponseTimeInMs { get; set; }

        public string AppVersion { get; set; }

        public string AdditionalInfo { get; set; }
    }
}
