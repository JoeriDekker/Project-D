using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WAMServer.Models{

    /// <summary>
    /// The User class represents a user in the database.
    /// </summary>
    public class WaterStorage
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("ControlPC")]
        public Guid controlPCID { get; set; }

        [Required]
        public string typeStorage { get; set; } = null!;

        [Required]
        public decimal waterStored { get; set; } = 0;

        public string regio {get;set;} = null!;

        public int state {get;set;} = 0;

        public ControlPC ControlPC {get;set;} = null!;

        /// <summary>
        /// Constructor for creating a WaterStorage object.
        /// </summary>
        /// <param name="controlPCID">ID of the control PC associated with the log.</param>
        /// <param name="waterStored">amount of water stored.</param>
        /// <param name="regio">region of the water storage.</param>
        public WaterStorage(Guid controlPCID, string typeStorage, decimal waterStored, string regio, int state)
        {
            this.Id = Guid.NewGuid();
            this.controlPCID = controlPCID;
            this.typeStorage = typeStorage;
            this.waterStored = waterStored;
            this.regio = regio;
            this.state = state;
        }

    }

}