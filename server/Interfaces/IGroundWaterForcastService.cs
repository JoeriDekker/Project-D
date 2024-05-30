using WAMServer.Models;

namespace WAMServer.Interfaces
{
    public interface IGroundWaterForecastService
    {
        public decimal GetGroundWaterForecastForTomorrow(decimal currentWaterLevel, decimal houseArea, string place);
    }
}