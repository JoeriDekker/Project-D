using WAMServer.Interfaces;

namespace WAMServer.Services
{
    public class GroundWaterForecastService : IGroundWaterForecastService
    {
        private readonly IConfiguration configuration;
        // The density of water in kg/m^3
        private readonly int waterDensity = 997;
        private readonly decimal heatOfVaporization = (decimal)2.45 * (decimal)Math.Pow(10, 6);
        
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

        public GroundWaterForecastService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public decimal GetGroundWaterForecastForTomorrow(decimal currentWaterLevel, decimal houseArea, string place)
        {
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