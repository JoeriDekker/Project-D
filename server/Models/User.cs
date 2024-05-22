using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BCrypt.Net;

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
        [Required]
        public bool IsConfirmed { get; set; } = false;

        [Required]
        public Guid ConfirmationToken { get; set; }
        public Guid? AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address? Address { get; set; }


        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="firstName">The firstname of the user</param>
        /// <param name="lastName">The lastname of the user</param>
        /// <param name="email">The emailaddress of the user</param>
        /// <param name="password">The hashed password of the user</param>
        public User(string firstName, string lastName, string email, string password) : this(Guid.NewGuid(), firstName, lastName, email, password, false, Guid.NewGuid(), null)
        {
        }

        public User(Guid id, string firstName, string lastName, string email, string password, bool isConfirmed, Guid confirmationToken, Guid? addressId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            IsConfirmed = isConfirmed;
            ConfirmationToken = confirmationToken;
            AddressId = addressId;
        }
    }
}
