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
            throw new NotImplementedException();
        }

        public IEnumerable<WaterStorage?> GetAll(Func<WaterStorage, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WaterStorage?>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WaterStorage?> UpdateAsync(WaterStorage entity, Func<WaterStorage, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }

}