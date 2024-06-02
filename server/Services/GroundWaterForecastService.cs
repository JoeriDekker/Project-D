using WAMServer.Interfaces.Services;
using WAMServer.Records;
using WAMServer.Services.Weather;

namespace WAMServer.Services
{
    public class GroundWaterForecastService : IGroundWaterForecastService
    {
        // The density of water in kg/m^3
        private readonly int waterDensity = 997;
        private readonly decimal heatOfVaporization = (decimal)2.45 * (decimal)Math.Pow(10, 6);

        private readonly BuienradarService  _buienradarService;

        public GroundWaterForecastService(HttpClient httpClient, BuienradarService? buienradarService = null)
        {
            _buienradarService = buienradarService ?? new BuienradarService(httpClient);
        }
        
        private readonly Dictionary<string, double> shortWaveRadiation = new Dictionary<string, double>
        {
            {"jan", 3.2},
            {"feb", 5.5},
            {"mar", 8.8},
            {"apr", 12.5},
            {"may", 15.4},
            {"jun", 16.6},
            {"jul", 16.0},
            {"aug", 13.6},
            {"sep", 10.2},
            {"oct", 6.7},
            {"nov", 3.9},
            {"dec", 2.6}
        };

        public async Task<decimal> GetGroundWaterForecastForTomorrow(decimal currentWaterLevel, decimal houseArea, string place)
        {
            BuienradarData buienradarData = await _buienradarService.GetBuienradarDataAsync(place);
            if (buienradarData == null)
            {
                return decimal.MinValue;
            }

            // TODO: Find a way to get humidity data from outside, as buienradar does not provide it
            // TODO: Find a more fine-grained way to get the number of sunhours.
            var grassEvaporation = grassEvaporation(buienradarData.MaxTemperatureAvg,
            0.70, 
            buienradarData.WindSpeed, buienradarData.HoursOfSun,
            buienradarData.MaximalHoursOfSun, 
            DateTime.Now.Month);

            return 0;
        }

        private decimal grassEvaporation(decimal temperature, decimal humidity, decimal windSpeed, int hoursOfSun, int maximalHoursOfSun, int currentMonth)
        {
            var saturationVaporPressure = calculateSaturationVaporPressure(temperature);
            var actualVaporPressure = humidity * saturationVaporPressure;
            var aerodynamicResistance = calculateAerodynamicResistance(windSpeed);
            decimal grassReflectionCoefficient = (decimal)0.24;
            var netRadiationDividedByYP = 0;
            return 0;
        }

        // Helper functions
        private decimal calculateSaturationVaporPressure(decimal temperature)
        {
            double temp_double = (double)temperature;
            var res = 0.61 * Math.Exp((19.9 * temp_double) / (temp_double + 273));
            return (decimal)res;
        }

        private decimal calculateAerodynamicResistance(decimal windSpeed)
        {
            return (245) / ((decimal)0.54 * windSpeed + (decimal)0.5) * (1 / 86400);
        }

    }
}