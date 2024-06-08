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
        public Guid ControlPCID { get; set; }

        [Required]
        public string TypeStorage { get; set; } = null!;

        [Required]
        public decimal WaterStored { get; set; } = 0;

        public string Regio {get;set;} = null!;

        // 1 == active, 2 == inactive, 3 == error
        public int StorageState {get;set;} = 0;

        public ControlPC ControlPC {get;set;} = null!;

        /// <summary>
        /// Constructor for creating a WaterStorage object.
        /// </summary>
        /// <param name="controlPCID">ID of the control PC associated with the log.</param>
        /// <param name="waterStored">amount of water stored.</param>
        /// <param name="regio">region of the water storage.</param>
        public WaterStorage(Guid ControlPCID, string TypeStorage, decimal WaterStored, string Regio, int StorageState)
        {
            this.Id = Guid.NewGuid();
            this.ControlPCID = ControlPCID;
            this.TypeStorage = TypeStorage;
            this.WaterStored = WaterStored;
            this.Regio = Regio;
            this.StorageState = StorageState;
        }

    }

}