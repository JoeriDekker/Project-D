using WAMServer.Models;

namespace WAMServer.Interfaces{
    public interface IGroundWaterLogRepository<T>{
        public GroundWaterLog? GetGroundWaterLog(Guid controlpcid);
    }
       
}