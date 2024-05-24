using WAMServer.Interfaces;
using WAMServer.Models;
using Microsoft.EntityFrameworkCore;

namespace WAMServer.Repositories{

    public class DbActionLogRepository : IRepository<ActionLog>
    {

        private readonly WamDBContext _context;

        public DbActionLogRepository(WamDBContext context)
        {
            _context = context;
        }

        public Task<ActionLog> AddAsync(ActionLog entity)
        {
            throw new NotImplementedException();
        }

        public Task<ActionLog?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ActionLog? Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ActionLog?> GetAll(Func<ActionLog, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ActionLog?>> GetAllAsync()
        {
           return await _context.ActionLog.Include(a => a.ActionType).ToListAsync();
        }

        public Task<ActionLog?> UpdateAsync(ActionLog entity, Func<ActionLog, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}