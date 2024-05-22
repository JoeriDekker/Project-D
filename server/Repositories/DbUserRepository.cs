
using WAMServer.Interfaces;
using WAMServer.Models;


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

        /// <summary>
        /// Adds a user to the database in an asynchronous manner.
        /// </summary>
        /// <param name="entity">The entity to be added, in this case the user.</param>
        /// <returns>The created user.</returns>
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

        /// <summary>
        /// Gets a user from the database by it's id.
        /// </summary>
        /// <param name="id">The id of the user to be fetched</param>
        /// <returns>Returns the user from the database if it exists, otherwise a null.</returns>
        public User? Get(Guid id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User?> GetAll(Func<User, bool> predicate)
        {
            return _context.Users.Where(predicate).ToList();
        }

        public async Task<User?> UpdateAsync(User entity, Func<User, bool> predicate)
        {
            var user = _context.Users.Where(predicate).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("User not found");
            }
            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.Email = entity.Email;
            user.Password = entity.Password;
            user.IsConfirmed = entity.IsConfirmed;
            user.ConfirmationToken = entity.ConfirmationToken;
            user.AddressId = entity.AddressId;
            await _context.SaveChangesAsync();
            return user;
        }
    }
}