using WAMServer.Interfaces;
using WAMServer.Models;
using Microsoft.EntityFrameworkCore;

namespace WAMServer.Repositories{

    public class DbControlPCRepository : IRepository<ControlPC>
    {

        private readonly WamDBContext _context;

        public DbControlPCRepository(WamDBContext context)
        {
            _context = context;
        }

        public Task<ControlPC> AddAsync(ControlPC entity)
        {
            throw new NotImplementedException();
        }

        public Task<ControlPC?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ControlPC? Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ControlPC?> GetAll(Func<ControlPC, bool> predicate)
        {
            return _context.ControlPCs.Where(predicate).ToList();
        }

        public Task<IEnumerable<ControlPC?>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ControlPC?> UpdateAsync(ControlPC entity, Func<ControlPC, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}