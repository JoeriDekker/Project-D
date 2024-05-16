using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WAMServer.Models
{
    /// <summary>
    /// The User class represents a user in the database.
    /// </summary>
    public class ActionType
    {
        [Key]
        public Guid Id { get; set; }

        [Required, Column(TypeName = "varchar(255)")]
        public string title { get; set; }

        [Required, Column(TypeName = "varchar(255)")]
        public string details {get; set;}

        /// <summary>
        /// Constructor for creating an ActionType object.
        /// </summary>
        /// <param name="title">Title of the action type.</param>
        /// <param name="details">Details of the action type.</param>
        public ActionType(string title, string details)
        {
            this.Id = Guid.NewGuid();
            this.title = title;
            this.details = details;
        }

    }
}
