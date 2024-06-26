using WAMServer.DTO;
using WAMServer.Models;

namespace WAMServer.Interfaces
{
    // TODO: Make this interface generic, call it IRepository<T> and make all other repositories inherit from it
    public interface IUserRepository
    {
        User? GetUser(Guid id);
        User? GetUser(string email);
        User? GetUserIncludingAddress(User? user);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<User> DeleteUserAsync(int id);
    }
}
