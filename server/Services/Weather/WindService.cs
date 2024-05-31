using HtmlAgilityPack;
using WAMServer.Interfaces.Services.Weather;

namespace WAMServer.Services.Weather
{
    public class WindService : IWindService
    {

        private readonly ILogger<WindService> Logger;
        private readonly HttpClient Client;

        public WindService(ILogger<WindService> logger, HttpClient client)
        {
            Logger = logger;
            Client = client;
            Client.BaseAddress = new Uri("https://gadgets.buienradar.nl/gadget/radarfivedays");
        }

    public async Task<decimal> GetWindSpeedForTomorrow(string place)
    {
        try
        {
            using HttpResponseMessage response = await Client.GetAsync("");
            if (!response.IsSuccessStatusCode)
            {
                return 0;
            }

            var content = await response.Content.ReadAsStringAsync();
            HtmlDocument htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(content);

            var tomorrowForecast = htmlSnippet.DocumentNode.SelectSingleNode("//div[contains(@class, 'forecast')][2]");
            if (tomorrowForecast == null)
            {
                return -1;
            }

            var windSpeedBftText = tomorrowForecast.ChildNodes[11].InnerText.Trim();
            if (!int.TryParse(windSpeedBftText, out int windSpeedBft))
            {
                return -1;
            }

            return BeaufortToMs(windSpeedBft);
        }
        catch (Exception ex)
        {
            // Log exception
            Logger.LogError("Failed to get wind speed for tomorrow. Exception occurred: {0}", ex.Message);
            return -1;
        }
    }
    private decimal BeaufortToMs(int bft)
    {
        return Math.Round((decimal)0.836 * (decimal)Math.Sqrt(Math.Pow(bft, 3)) * 100) / 100;
    }
}
}