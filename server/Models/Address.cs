using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WAMServer.Models
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; }
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Zip { get; set; } = null!;
        public Guid? UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
