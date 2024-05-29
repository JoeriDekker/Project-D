using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WAMServer.Models
{
    /// <summary>
    /// The ControlPC model is used to store the settings of a control PC.
    /// </summary>
    public class ControlPC
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required, Column(TypeName = "varchar(255)")]
        public string Secret { get; set; }


        [Required, Column(TypeName = "varchar(255)")]
        public string MeetputBroID { get; set; }

        [Required, Column(TypeName = "varchar(255)")]
        public string ControlPCSecret { get; set; }

        public decimal HouseArea { get; set; } = 125;

        public User User { get; set; } = null!;

        /// <summary>
        /// Constructor for creating a ControlPC object.
        /// </summary>
        /// <param name="userId">ID of the user associated with the ControlPC.</param>
        /// <param name="secret">Secret associated with the ControlPC.</param>
        /// <param name="meetputBroID">MeetputBro ID associated with the ControlPC.</param>
        /// <param name="ControlPCSecret">Secret of the ControlPC.</param>
        /// <param name="houseArea">Area of the house associated with the ControlPC. Defaults to 125 when left empty</param>
        public ControlPC(Guid userId, string secret, string meetputBroID, string controlPCSecret, decimal houseArea = 125)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Secret = secret;
            MeetputBroID = meetputBroID;
            ControlPCSecret = controlPCSecret;
            HouseArea = houseArea;
        }

    }
}
