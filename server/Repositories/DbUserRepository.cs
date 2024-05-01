
using Microsoft.EntityFrameworkCore;
using WAMServer.DTO;
using WAMServer.Interfaces;
using WAMServer.Models;

namespace WAMServer.Repositories
{
    /// <summary>
    /// This class is responsible for handling the database operations for the User entity.
    /// </summary>
    public class DbUserRepository : IUserRepository
    {
        private readonly WamDBContext _context;

        public DbUserRepository(WamDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a user to the database.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <returns>The added user.</returns>
        public async Task<User> AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Deletes a user from the database.
        /// </summary>
        /// <param name="id">The id of the user to delete.</param>
        /// <returns>The deleted user.</returns>
        public Task<User> DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a user from the database.
        /// </summary>
        /// <param name="id">The id of the user to get.</param>
        /// <returns>The user with the given id.</returns>
        public User? GetUser(Guid id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Gets a user from the database.
        /// </summary>
        /// <param name="email">The email of the user to get.</param>
        /// <returns>The user with the given email.</returns>
        public User? GetUser(string email)
        {
            return _context.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        /// <summary>
        /// Gets a user from the database including the address.
        /// </summary>
        /// <param name="user">The user to get.</param>
        /// <returns>The user with the given id including the address.</returns>
        public User? GetUserIncludingAddress(User? user)
        {
            if (user == null)
            {
                return null;
            }
            return _context.Users.Include(u => u.Address).Where(u => u.Id == user.Id).FirstOrDefault();
        }

        public Task<IEnumerable<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

    }
}