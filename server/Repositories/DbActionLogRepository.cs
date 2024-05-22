using WAMServer.Interfaces;
using WAMServer.Models;
using Microsoft.EntityFrameworkCore;

namespace WAMServer.Repositories{

    public class DbActionLogRepository : IRepository<ActionLog>
    {
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

        public Task<IEnumerable<ActionLog?>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ActionLog?> UpdateAsync(ActionLog entity, Func<ActionLog, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}