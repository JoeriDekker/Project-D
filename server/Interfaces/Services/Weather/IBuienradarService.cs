using WAMServer.Records;

namespace WAMServer.Interfaces.Services.Weather
{
    /// <summary>
    /// Interface for the Buienradar service.
    /// </summary>
    public interface IBuienradarService
    {
        /// <summary>
        /// Retrieves the Buienradar data for a given location.
        /// </summary>
        /// <param name="location">The location to retrieve the data for.</param>
        /// <returns>The Buienradar data for the given location.</returns>
        Task<BuienradarData?> GetBuienradarDataAsync(string location);
    }
}