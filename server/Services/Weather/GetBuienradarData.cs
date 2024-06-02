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

        public async Task<BuienradarData?> GetBuienradarDataAsync(string location)
        {
            var response = _client.GetAsync("");
            if (!response.Result.IsSuccessStatusCode) return null;
            var content = await response.Result.Content.ReadAsStringAsync();
            JObject? data = JsonConvert.DeserializeObject<JObject>(content);
            if (data == null) return null;
            var tomorrowsData = data.GetValue("forecast")?.SelectTokens("fivedayforecast").ToList()[1];
            if (tomorrowsData == null) return null;
            var rain = ((int)tomorrowsData.SelectToken("mmRainMin") + (int)tomorrowsData.SelectToken("mmRainMax")) / 2;
            var minTemperatureAvg = (int)tomorrowsData.SelectToken("minTemperature");
            var maxTemperatureMin = (int)tomorrowsData.SelectToken("maxtemperatureMin");
            var maxTemperatureMax = (int)tomorrowsData.SelectToken("maxtemperatureMax");
            var maxTemperatureAvb = (maxTemperatureMin + maxTemperatureMax) / 2;

            var wind = (int)tomorrowsData.SelectToken("wind");

            return new BuienradarData()
            {
                MaxTemperatureAvg = maxTemperatureAvb,
                MinTemperatureAvg = minTemperatureAvg,
                MMRain = rain,
                WindSpeed = wind
            };

        }
    }
}