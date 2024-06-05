using WAMServer.Models;


namespace WAMServer.Interfaces
{
    public interface IWaterLevelSettings
    {
        WaterLevelSettings? GetByUserId(Guid id);
 
    }
}