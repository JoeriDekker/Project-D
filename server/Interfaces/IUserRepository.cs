public interface IUserRepository
{
    Task<UserDTO> GetUserAsync(int id);
    Task<UserDTO> GetUserAsync(string email);
    Task<IEnumerable<UserDTO>> GetUsersAsync();
    Task<UserDTO> AddUserAsync(User user);
    Task<UserDTO> UpdateUserAsync(User user);
    Task<UserDTO> DeleteUserAsync(int id);
}