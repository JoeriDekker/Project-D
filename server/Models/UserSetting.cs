using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WAMServer.Models
{
    /// <summary>
    /// The User class represents a user in the database.
    /// </summary>
    public class UserSetting
    {
        [Key]
        public Guid Id { get; set; }
        [Required, Column(TypeName = "varchar(100)")]

        public Guid userId { get; set; }
        [Required, Column(TypeName = "varchar(100)")]
        public Guid controlPCID { get; set; }
        [Required, Column(TypeName = "varchar(100)")]
        public string controlPCSecret { get; set; }

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="firstName">The firstname of the user</param>
        /// <param name="lastName">The lastname of the user</param>
        /// <param name="email">The emailaddress of the user</param>
        /// <param name="password">The password of the user</param>
        public UserSetting(Guid userId, Guid controlPCID, string controlPCSecret)
        {
            this.Id = Guid.NewGuid();
            this.userId = userId;
            this.controlPCID = controlPCID;
            this.controlPCSecret = controlPCSecret;
        }
    }
}
