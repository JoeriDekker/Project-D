using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WAMServer.Models
{
    /// <summary>
    /// The User class represents a user in the database.
    /// </summary>
    public class User
    {
        /// <summary>
        /// The ID of the user.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// The first name of the user.
        /// </summary>
        [Required, Column(TypeName = "varchar(100)"), MaxLength(100)]
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// The last name of the user.
        /// </summary>
        [Required, Column(TypeName = "varchar(100)"), MaxLength(100)]
        public string LastName { get; set; } = null!;

        /// <summary>
        /// The email address of the user.
        /// </summary>
        [Required, Column(TypeName = "varchar(100)"), MaxLength(100), EmailAddress]
        public string Email { get; set; } = null!;

        /// <summary>
        /// The hashed password of the user.
        /// </summary>
        [Required, Column(TypeName = "varchar(100)"), MaxLength(100)]
        public string Password { get; set; } = null!;

        /// <summary>
        /// The confirmation status of the user.
        /// </summary>
        [Required]
        public bool IsConfirmed { get; set; } = false;

        /// <summary>
        /// The ID of the user setting.
        /// </summary>
        public Guid? UserSettingId { get; set; }

        /// <summary>
        /// The confirmation token of the user.
        /// </summary>
        [Required]
        public Guid ConfirmationToken { get; set; }

        /// <summary>
        /// The ID of the address of the user.
        /// </summary>
        public Guid? AddressId { get; set; }

        /// <summary>
        /// The ID of the control PC of the user.
        /// </summary>
        public Guid? ControlPCId { get; set; }

        // Navigation properties

        /// <summary>
        /// The address of the user.
        /// </summary>
        public Address? Address { get; set; }

        /// <summary>
        /// The action logs associated with the user.
        /// </summary>
        public List<ActionLog> ActionLogs { get; set; } = new();

        /// <summary>
        /// The user settings.
        /// </summary>
        public UserSetting? UserSetting { get; set; }

        /// <summary>
        /// The control PC of the user.
        /// </summary>
        public ControlPC? ControlPC { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Constructor with essential user details.
        /// </summary>
        /// <param name="firstName">The first name of the user</param>
        /// <param name="lastName">The last name of the user</param>
        /// <param name="email">The email address of the user</param>
        /// <param name="password">The plain text password of the user</param>
        public User(string firstName, string lastName, string email, string password)
            : this(Guid.NewGuid(), firstName, lastName, email, BCrypt.Net.BCrypt.HashPassword(password), false, Guid.NewGuid(), null, null, null)
        {
        }

        /// <summary>
        /// Full constructor.
        /// </summary>
        /// <param name="id">The ID of the user</param>
        /// <param name="firstName">The first name of the user</param>
        /// <param name="lastName">The last name of the user</param>
        /// <param name="email">The email address of the user</param>
        /// <param name="password">The hashed password of the user</param>
        /// <param name="isConfirmed">The confirmation status of the user</param>
        /// <param name="confirmationToken">The confirmation token of the user</param>
        /// <param name="addressId">The ID of the address of the user</param>
        /// <param name="userSettingId">The ID of the user setting</param>
        /// <param name="controlPCId">The ID of the control PC</param>
        public User(Guid id, string firstName, string lastName, string email, string password, bool isConfirmed, Guid confirmationToken, Guid? addressId, Guid? userSettingId, Guid? controlPCId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            IsConfirmed = isConfirmed;
            ConfirmationToken = confirmationToken;
            AddressId = addressId;
            UserSettingId = userSettingId;
            ControlPCId = controlPCId;
        }
    }
}
