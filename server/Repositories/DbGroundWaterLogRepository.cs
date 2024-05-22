using WAMServer.Interfaces;
using WAMServer.Models;
using Microsoft.EntityFrameworkCore;

namespace WAMServer.Repositories{

    public class DbGroundWaterLogRepository : IRepository<GroundWaterLog>
    {

        private WamDBContext _context;

        public DbGroundWaterLogRepository(WamDBContext context)
        {
            _context = context;
        }

        public async Task<GroundWaterLog> AddAsync(GroundWaterLog entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<GroundWaterLog> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public GroundWaterLog? Get(Guid id)
        {
            return _context.GroundWaterLog.Where(a => a.Id == id).FirstOrDefault();
        }

        public async Task<IEnumerable<GroundWaterLog>> GetAllAsync()
        {
            return await _context.GroundWaterLog.ToListAsync();
        }

        // TODO: understand why it has to be 2 params instead of 1
        public Task<GroundWaterLog> UpdateAsync(GroundWaterLog entity, Func<GroundWaterLog, bool> predicate)
        {
           throw new NotImplementedException();
        }

        public Task<GroundWaterLog> UpdateAsync(GroundWaterLog entity)
        {
            throw new NotImplementedException();
        }
    }
}