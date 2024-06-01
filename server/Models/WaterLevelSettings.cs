using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WAMServer.Records.Bodies;

namespace WAMServer.Models
{

    public class WaterLevelSettings
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid userId { get; set; }

        [Required, Column(TypeName = "decimal")]
        public decimal? PoleHeight { get; set; } = 0;

        [Required, Column(TypeName = "decimal")]
        public decimal? IdealHeight { get; set; } = 0;
        public User? User { get; set; }


        public WaterLevelSettings(WaterLevelSettingsPatchBody waterlevelsettingsPatchBody)
        {
            PoleHeight = waterlevelsettingsPatchBody.PoleHeight!;
            IdealHeight = waterlevelsettingsPatchBody.IdealHeight!;
        }

    }
}
