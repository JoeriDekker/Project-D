namespace WAMServer.Interfaces.Services.Weather
{
    /// <summary>
    /// Interface for Wind Service
    /// </summary>
    public interface IWindService
    {
        /// <summary>
        /// Get Wind Speed for Tomorrow
        /// </summary>
        /// <param name="place">The place to get the wind speed for</param>
        public Task<decimal> GetWindSpeedForTomorrow(string place);
    }
}