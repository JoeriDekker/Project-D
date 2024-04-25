

public class DbUserRepository : IUserRepository
{
    private readonly WamDBContext _context;

    public DbUserRepository(WamDBContext context)
    {
        _context = context;
    }

    public async Task<UserDTO> AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        return new UserDTO(user.Id, user.FirstName, user.LastName, user.Email, user.Address);
    }

    public Task<UserDTO> DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO> GetUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO> GetUserAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserDTO>> GetUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO> UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}