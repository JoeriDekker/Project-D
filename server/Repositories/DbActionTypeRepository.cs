using WAMServer.Interfaces;
using WAMServer.Models;
using Microsoft.EntityFrameworkCore;

namespace WAMServer.Repositories{

    public class DbActionTypeRepository : IRepository<ActionType>
    {
        public Task<ActionType> AddAsync(ActionType entity)
        {
            throw new NotImplementedException();
        }

        public Task<ActionType?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ActionType? Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ActionType?>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ActionType?> UpdateAsync(ActionType entity, Func<ActionType, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}