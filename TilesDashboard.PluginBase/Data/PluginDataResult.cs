namespace TilesDashboard.PluginBase.Data
{
    /// <summary>
    /// Base class which represents output of each plugin.
    /// </summary>
    public class PluginDataResult
    {
        protected PluginDataResult(Status status)
        {
            Status = status;
        }

        /// <summary>
        /// Status of getting data.
        /// OK - data should be saved.
        /// NoUpdate - no new data, saving operation will be to performed.
        /// </summary>
        public Status Status { get; }

        public string ErrorMessage { get; private set; }

        public PluginDataResult WithErrorMessage(string errorMessage)
        {
            ErrorMessage = errorMessage;
            return this;
        }
    }
}
