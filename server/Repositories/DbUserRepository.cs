
using WAMServer.Interfaces;
using WAMServer.Models;


// TODO: implement IRepository instead of IUserRepository
namespace WAMServer.Repositories
{
    /// <summary>
    /// This class is responsible for handling the database operations for the User entity.
    /// </summary>
    public class DbUserRepository : IRepository<User>
    {
        private readonly WamDBContext _context;

        public DbUserRepository(WamDBContext context)
        {
            _context = context;
        }

        public Task<User> AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<User> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public User? Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}