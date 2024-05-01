using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WAMServer.Models
{
    /// <summary>
    /// Address model
    /// </summary>
    public class Address
    {
        /// <summary>
        /// The unique identifier of the address.
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// The street of the address.
        /// </summary>
        public string Street { get; set; } = null!;
        /// <summary>
        /// The city of the address.
        /// </summary>
        public string City { get; set; } = null!;
        /// <summary>
        /// The state of the address.
        /// </summary>
        public string Zip { get; set; } = null!;
        /// <summary>
        /// The country of the address.
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        /// The user of the address.
        /// </summary>
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
