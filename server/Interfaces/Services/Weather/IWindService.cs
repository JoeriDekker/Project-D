namespace WAMServer.Interfaces.Services.Weather
{
    public interface IWindService
    {
        public Task<decimal> GetWindSpeedForTomorrow(string place);
    }
}