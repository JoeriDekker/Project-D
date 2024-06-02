namespace WAMServer.Interfaces.Services.Weather
{
    public interface IWindService
    {
        public Task<decimal> GetWindSpeedForTomorrowAsync(string place);
    }
}