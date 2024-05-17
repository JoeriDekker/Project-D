using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WAMServer.Models
{
    /// <summary>
    /// The User class represents a user in the database.
    /// </summary>
    public class GroundWaterLog
    {
        [Key]
        public Guid Id { get; set; }
        [Required, Column(TypeName = "varchar(100)")]
        public string controlPCID { get; set; } = null!;
        [Required, Column(TypeName = "varchar(100)")]
        public string date { get; set; } = null!;
        [Required, Column(TypeName = "decimal")]
        public decimal level { get; set; } = 0;

        /// <summary>
        /// Constructor for creating a GroundWaterLog object.
        /// </summary>
        /// <param name="controlPCID">ID of the control PC associated with the log.</param>
        /// <param name="description">Description of the groundwater log.</param>
        /// <param name="level">Level of groundwater.</param>
        public GroundWaterLog(string controlPCID, string date, decimal level)
        {
            this.Id = Guid.NewGuid();
            this.controlPCID = controlPCID;
            this.date = date;
            this.level = level;
        }

    }
}
