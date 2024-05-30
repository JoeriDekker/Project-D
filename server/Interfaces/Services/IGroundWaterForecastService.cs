using WAMServer.Models;

namespace WAMServer.Interfaces.Services
{
    public interface IGroundWaterForecastService
    {
        public Task<decimal> GetGroundWaterForecastForTomorrow(decimal currentWaterLevel, decimal houseArea, string place);
    }
}