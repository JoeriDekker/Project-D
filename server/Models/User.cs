using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WAMServer.Models
{
    /// <summary>
    /// The User class represents a user in the database.
    /// </summary>
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required, Column(TypeName = "varchar(100)")]
        public string FirstName { get; set; } = null!;
        [Required, Column(TypeName = "varchar(100)")]
        public string LastName { get; set; } = null!;
        [Required, Column(TypeName = "varchar(100)")]
        public string Email { get; set; } = null!;
        [Required, Column(TypeName = "varchar(100)")]
        public string Password { get; set; } = null!;
        [ForeignKey("Address")]
        public Guid? AddressId { get; set; }
        public Address? Address { get; set; }

        public List<ActionLog> ActionLogs { get; set; } = null!;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="firstName">The firstname of the user</param>
        /// <param name="lastName">The lastname of the user</param>
        /// <param name="email">The emailaddress of the user</param>
        /// <param name="password">The password of the user</param>
        public User(string firstName, string lastName, string email, string password)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }
    }
}
