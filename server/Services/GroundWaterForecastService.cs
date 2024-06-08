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
        
        private readonly Dictionary<int, double> shortWaveRadiation = new Dictionary<int, double>
        {
            {1, 3.2},
            {2, 5.5},
            {3, 8.8},
            {4, 12.5},
            {5, 15.4},
            {6, 16.6},
            {7, 16.0},
            {8, 13.6},
            {9, 10.2},
            {10, 6.7},
            {11, 3.9},
            {12, 2.6}
        };

        public async Task<decimal> GetGroundWaterForecastForTomorrow(decimal currentWaterLevel, decimal houseArea, string place)
        {
            BuienradarData? buienradarData = await _buienradarService.GetBuienradarDataAsync(place);
            if (buienradarData == null)
            {
                return decimal.MinValue;
            }

            // TODO: Find a way to get hours of sun
            var amountOfWaterEvaporated = grassEvaporation(
                buienradarData.Temperature,
                buienradarData.Humidity,
                buienradarData.WindSpeed,
                buienradarData.SunChance / 100 * 12,
                12,
                DateTime.Now.Month
            ) * houseArea;
            // TODO: Create user input for amount of trees in nearby area
            int trees = 3;
            var amountOfWaterAbsorbedByTrees = trees * 70;

            var currentWaterVolume = currentWaterLevel * houseArea;

            var deltaVolume = currentWaterVolume - amountOfWaterEvaporated - amountOfWaterAbsorbedByTrees;

            var result = deltaVolume / houseArea;


            return result;
        }

        private decimal grassEvaporation(int temperature, int humidity, int windSpeed, int hoursOfSun, int maximalHoursOfSun, int currentMonth)
        {
            var saturationVaporPressure = calculateSaturationVaporPressure(temperature);
            var actualVaporPressure = humidity * saturationVaporPressure;
            var aerodynamicResistance = calculateAerodynamicResistance(windSpeed);
            var hellingDampspanningsKromme = CalculateHellingVanDeDampspanningskromme(temperature);
            decimal grassReflectionCoefficient = 0.24m;
            var Rn = NettoStralingOpAardoppervlakGedeeldDoorYp(
                hoursOfSun,
                maximalHoursOfSun,
                grassReflectionCoefficient,
                temperature,
                humidity,
                currentMonth
            );
            var result = (((decimal)hellingDampspanningsKromme * Rn)
                + 1004m * 1.205m / (waterDensity * heatOfVaporization) * (decimal)((saturationVaporPressure - actualVaporPressure)
                / (decimal)aerodynamicResistance)) / ((decimal)hellingDampspanningsKromme + 0.66m);
            return result;
        }

        // Helper functions
        private decimal calculateSaturationVaporPressure(decimal temperature)
        {
            double temp_double = (double)temperature;
            var res = 0.61 * Math.Exp(19.9 * temp_double / (temp_double + 273));
            return (decimal)res;
        }

        private decimal calculateAerodynamicResistance(decimal windSpeed)
        {
            decimal part1 = (decimal)0.54 * (decimal)windSpeed + (decimal)0.5;
            decimal part2 = (1.0m / 86400);
            decimal res = 245 / part1 * part2;
            return res;
        }

        private decimal NettoStralingOpAardoppervlakGedeeldDoorYp(double zonnenuren, double maximalezonnenuren, decimal reflectiecoefficient, int temperatuur, int luchtvochtigheid, int month)
        {
            decimal Rc = (decimal)((0.2 + 0.48 * (zonnenuren / maximalezonnenuren)) * shortWaveRadiation[month]);
            decimal Ea = calculateSaturationVaporPressure(temperatuur) * (luchtvochtigheid / 100);
            decimal Rb = (decimal)((0.0049 * Math.Pow((temperatuur + 273), 4)) * (0.47 - 0.21 * Math.Sqrt((double)Ea)) * (0.2 + 0.8 * (zonnenuren / maximalezonnenuren)));
            decimal RbgedeelddoorYP = Rb / (waterDensity * heatOfVaporization) * (decimal)Math.Pow(10, 3);
            return (1 - reflectiecoefficient) * Rc - RbgedeelddoorYP;
        }

        private double CalculateHellingVanDeDampspanningskromme(int temperature)
        {
            return (double)(5430 * calculateSaturationVaporPressure(temperature)) / Math.Pow(temperature + 273, 2);
        }

    }
}