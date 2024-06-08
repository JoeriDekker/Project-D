using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WAMServer.Interfaces.Services.Weather;
using WAMServer.Records;

namespace WAMServer.Services.Weather
{
    /// <summary>
    /// Service for handling Buienradar data.
    /// </summary>
    public class BuienradarService : IBuienradarService
    {
        private readonly HttpClient _client;

        public BuienradarService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://data.buienradar.nl/2.0/feed/json");
        }

        /// <summary>
        /// Get Buienradar data for a location.
        /// </summary>
        /// <param name="location">The location to get the data for.</param>
        /// <returns>The Buienradar data for the location.</returns>
        public async Task<BuienradarData?> GetBuienradarDataAsync(string location)
        {
            var currentWeatherData = await getCurrentMeasurementForLocation(location);
            if (currentWeatherData == null) return null;
            var weatherForecastData = await getTodaysForecastForLocation();
            if (weatherForecastData == null) return null;
            return new BuienradarData()
            {
                Temperature = (int)currentWeatherData.Temperature,
                WindSpeed = (int)currentWeatherData.WindSpeed,
                MMRain = (int)(weatherForecastData.MmRainMin + weatherForecastData.MmRainMax) / 2,
                SunChance = weatherForecastData.SunChance,
                Humidity = (int)currentWeatherData.Humidity
            };

        }

        /// <summary>
        /// Get today's forecast.
        /// </summary>
        /// <returns>Today's forecast data.</returns>
        private async Task<WeatherForecast?> getTodaysForecastForLocation()
        {
            var response = _client.GetAsync("");
            if (!response.Result.IsSuccessStatusCode) return null;
            var content = await response.Result.Content.ReadAsStringAsync();
            JObject? data = JsonConvert.DeserializeObject<JObject>(content);
            if (data == null) return null;
            JToken? forecastData = data["forecast"]["fivedayforecast"];
            if (forecastData == null) return null;
            List<WeatherForecast>? fiveDayForecasts = JsonConvert.DeserializeObject<List<WeatherForecast>>(forecastData.ToString());
            if (fiveDayForecasts == null) return null;
            // Return today's forecast
            return fiveDayForecasts.First();
        }

        /// <summary>
        /// Get the current weather measurement for a location.
        /// </summary>
        /// <param name="location">The location to get the weather measurement for.</param>
        /// <returns>The current weather measurement for the location.</returns>
        private async Task<WeatherMeasurement?> getCurrentMeasurementForLocation(string location)
        {
            var response = _client.GetAsync("");
            if (!response.Result.IsSuccessStatusCode) return null;
            var content = await response.Result.Content.ReadAsStringAsync();
            JObject? data = JsonConvert.DeserializeObject<JObject>(content);
            if (data == null) return null;
            JToken? actualData = data["actual"];
            if (actualData == null) return null;
            JToken? stationMeasurementsData = actualData["stationmeasurements"];
            if (stationMeasurementsData == null) return null;
            List<WeatherMeasurement>? todaysData = JsonConvert.DeserializeObject<List<WeatherMeasurement>>(stationMeasurementsData.ToString());
            if (todaysData == null) return null;
            WeatherMeasurement? todaysDataForLocation = todaysData.FirstOrDefault(x => x.StationName.ToLower().Contains(location.ToLower()));
            if (todaysDataForLocation == null) return null;
            return todaysDataForLocation;
        }
    }
}