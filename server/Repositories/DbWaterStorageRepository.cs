using WAMServer.Interfaces;
using WAMServer.Models;
using Microsoft.EntityFrameworkCore;

namespace WAMServer.Repositories{

    public class DbWaterStorageRepository : IRepository<WaterStorage>
    {

        private readonly WamDBContext _context;

        public DbWaterStorageRepository(WamDBContext context)
        {
            _context = context;
        }

        public Task<WaterStorage> AddAsync(WaterStorage entity)
        {
            throw new NotImplementedException();
        }

        public Task<WaterStorage?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public WaterStorage? Get(Guid id)
        {   
            return _context.WaterStorage.Where(w => w.ControlPCID == id).FirstOrDefault();
        }

        public IEnumerable<WaterStorage?> GetAll(Func<WaterStorage, bool> predicate)
        {
            return _context.WaterStorage.Where(predicate).ToList();

        }

        public async Task<IEnumerable<WaterStorage?>> GetAllAsync()
        {
            return await _context.WaterStorage.ToListAsync();
        }

        public Task<WaterStorage?> UpdateAsync(WaterStorage entity, Func<WaterStorage, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }

}