using HtmlAgilityPack;
using WAMServer.Interfaces.Services.Weather;

namespace WAMServer.Services.Weather
{
    public class WindService : IWindService {
        public async Task<decimal> GetWindSpeedForTomorrow(string place)
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("https://gadgets.buienradar.nl/gadget/radarfivedays")
            };
            using HttpResponseMessage response = await client.GetAsync("");
            if (!response.IsSuccessStatusCode)
            {
                return 0;
            }
            var content = await response.Content.ReadAsStringAsync();
            HtmlDocument htmlsnippet = new HtmlDocument();
            htmlsnippet.LoadHtml(content);
            var tomorowsforecast = htmlsnippet.DocumentNode.SelectSingleNode("//div[contains(@class, 'forecast')][2]");
            var windspeedBftText = tomorowsforecast.ChildNodes[11].InnerHtml;
            int windspeedBft;
            if (!int.TryParse(windspeedBftText, out windspeedBft))
            {
                return -1;
            }
            // windspeed in m/s
            var windspeed = beaufortToMs(windspeedBft);
            return windspeed;
        }
        private decimal beaufortToMs(int bft) {
            return Math.Round((decimal)0.836 * (decimal)Math.Sqrt(Math.Pow(bft, 3)) * 100) / 100;
        }
    }
}