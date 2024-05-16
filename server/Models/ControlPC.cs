using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WAMServer.Models
{
    /// <summary>
    /// The User class represents a user in the database.
    /// </summary>
    public class ControlPC
    {
        [Key]
        public Guid Id { get; set; }

        [Required, Column(TypeName = "varchar(100)")]
        public Guid userId { get; set; }
        [Required, Column(TypeName = "varchar(255)")]
        public string secret { get; set; }

        [Required, Column(TypeName = "varchar(255)")]
        public string meetputBroID { get; set; }
        [Required, Column(TypeName = "varchar(255)")]
        public string ControlPCSecret { get; set; }


        /// <summary>
        /// Constructor for creating a ControlPC object.
        /// </summary>
        /// <param name="userId">ID of the user associated with the ControlPC.</param>
        /// <param name="secret">Secret associated with the ControlPC.</param>
        /// <param name="meetputBroID">MeetputBro ID associated with the ControlPC.</param>
        /// <param name="ControlPCSecret">Secret of the ControlPC.</param>
        public ControlPC(Guid userId, string secret, string meetputBroID, string controlPCSecret)
        {
            this.Id = Guid.NewGuid();
            this.secret = secret;
            this.meetputBroID = meetputBroID;
            this.ControlPCSecret = controlPCSecret;
        }

    }
}
