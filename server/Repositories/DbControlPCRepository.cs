using WAMServer.Interfaces;
using WAMServer.Models;
using Microsoft.EntityFrameworkCore;

namespace WAMServer.Repositories{

    public class DbControlPCRepository : IRepository<ControlPC>
    {
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