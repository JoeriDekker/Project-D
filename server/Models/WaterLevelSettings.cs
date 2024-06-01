using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WAMServer.Models
{

    public class WaterLevelSettings
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid userId { get; set; }

        [Required, Column(TypeName = "decimal")]
        public decimal PoleHeight { get; set; } = 0;

        [Required, Column(TypeName = "decimal")]
        public decimal IdealHeight { get; set; } = 0;

        public WaterLevelSettings(Guid userId, decimal PoleHeight, decimal IdealHeight)
        {
            this.Id = Guid.NewGuid();
            this.userId = userId;
            this.PoleHeight = PoleHeight;
            this.IdealHeight = IdealHeight;
        }

    }
}
