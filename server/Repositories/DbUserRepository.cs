
using Microsoft.EntityFrameworkCore;
using WAMServer.DTO;
using WAMServer.Interfaces;
using WAMServer.Models;

namespace WAMServer.Repositories
{
    public class DbUserRepository : IUserRepository
    {
        private readonly WamDBContext _context;

        public DbUserRepository(WamDBContext context)
        {
            _context = context;
        }

        public async Task<User> AddUserAsync(User user)
        {
            throw new NotImplementedException();
        }


        public Task<User> DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public User? GetUser(Guid id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public User? GetUser(string email)
        {
            return _context.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public User? GetUserIncludingAddress(User? user)
        {
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