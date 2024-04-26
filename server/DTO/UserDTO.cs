
using WAMServer.Models;

namespace WAMServer.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Address Address { get; set; } = null!;

        public UserDTO(Guid id, string firstName, string lastName, string email, Address address)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
        }

        public UserDTO(User user) : this(user.Id, user.FirstName, user.LastName, user.Email, user.Address)
        {}
    }
}
