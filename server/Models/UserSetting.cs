using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WAMServer.Models
{
    /// <summary>
    /// Model for the user setting. This model is used to store the settings of a user.
    /// </summary>
    public class UserSetting
    {
        /// <summary>
        /// The ID of the user setting. This ID is used to identify the user setting in the database.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// The ID of the user within the relation. This ID is used to identify the user within the relation.
        /// </summary>
        [ForeignKey("User"), Column(TypeName = "varchar(100)")]
        public Guid UserId { get; set; }
        
        /// <summary>
        /// The ID of the control PC in the house. This ID is used to identify the control PC in the house.
        /// </summary>
        [Required, Column(TypeName = "varchar(100)")]
        public Guid ControlPCId { get; set; }

        /// <summary>
        /// The secret token of the control PC. This token is used to authenticate the control PC with the server.
        /// </summary>
        [Required, Column(TypeName = "varchar(100)")]
        public string? ControlPCSecret { get; set; }

        /// <summary>
        /// Navigation property for the user of the setting.
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="userId">The id of the user within the relation</param>
        /// <param name="controlPCID">The ID of the control PC in the house</param>
        /// <param name="controlPCSecret">The secret token of the control PC</param>
        public UserSetting(Guid userId, Guid controlPCID, string? controlPCSecret = null)
        {
            this.Id = Guid.NewGuid();
            this.UserId = userId;
            this.ControlPCId = controlPCID;
            this.ControlPCSecret = controlPCSecret;
        }
    }
}
