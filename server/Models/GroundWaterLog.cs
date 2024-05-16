using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WAMServer.Models{

    public class GroundWaterLog{
        [Key]
        public Guid Id { get; set; }
        [Required, Column(TypeName = "varchar(100)")]
        public Guid? ControlPCId { get; set; }
        [ForeignKey("ControlPCId")]

        public string Date { get; set; } = null!;
        [Required, Column(TypeName = "varchar(100)")]

        public string Level { get; set; } = null!;
        [Required, Column(TypeName = "varchar(100)")]

        public string Description { get; set; } = null!;

        public GroundWaterLog(string date, Guid? controlPCId, string description, string level){
            Id = Guid.NewGuid();
            ControlPCId = controlPCId;
            Date = date;
            Description = description;
            Level = level;
        }
        
    }
}