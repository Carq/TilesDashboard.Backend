namespace TilesDashboard.PluginBase.WeatherPluginBase
{
    public class WeatherData
    {
        public WeatherData(decimal temperature, decimal huminidy)
        {
            Temperature = temperature;
            Huminidy = huminidy;
        }

        public WeatherData(decimal temperature)
        {
            Temperature = temperature;
        }

        public decimal Temperature { get; set; }

        public decimal? Huminidy { get; set; }
    }
}
