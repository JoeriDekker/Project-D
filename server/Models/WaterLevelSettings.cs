using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WAMServer.Records.Bodies;

namespace WAMServer.Models
{

    public class WaterLevelSettings
    {
        [Key]
        public Guid Id { get; set; }

        [Required, Column(TypeName = "decimal")]
        public decimal? PoleHeight { get; set; } = 0;

        [Required, Column(TypeName = "decimal")]
        public decimal? IdealHeight { get; set; } = 0;
        public Guid? UserId { get; set; }
        public User? User { get; set; }

        public WaterLevelSettings() {}

        public WaterLevelSettings(WaterLevelSettingsPatchBodyDecimal patch)
        {
            PoleHeight = patch.PoleHeight;
            IdealHeight = patch.IdealHeight;
        }
        
    }

}
