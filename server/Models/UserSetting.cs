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
        /// <param name="userId">The id of the user within the relation</param>
        /// <param name="controlPCID">The ID of the control PC in the house</param>
        /// <param name="controlPCSecret">The secret token of the control PC</param>
        public UserSetting(Guid userId, Guid controlPCID, string controlPCSecret)
        {
            this.Id = Guid.NewGuid();
            this.userId = userId;
            this.controlPCID = controlPCID;
            this.controlPCSecret = controlPCSecret;
        }
    }
}
