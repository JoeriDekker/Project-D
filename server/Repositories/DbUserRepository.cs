
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

        public async Task<User> AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<User> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public User? Get(Guid id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
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