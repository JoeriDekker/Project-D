using WAMServer.Models;


namespace WAMServer.Interfaces
{
    public interface IWaterLevelSettingsService
    {
        WaterLevelSettings? GetByUserId(Guid id);
 
    }
}