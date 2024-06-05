using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WAMServer.Models
{
    /// <summary>
    /// The User class represents a user in the database.
    /// </summary>
    public class ActionLog
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid userId { get; set; }
        [ForeignKey("ActionType")]
        public int actionTypeID { get; set; }

        [Required]
        public DateTime dateTimeStamp { get; set; }

        public User User {get;set;} = null!;
        public ActionType ActionType {get;set;} = null!;

        /// <summary>
        /// Constructor for creating an ActionLog object.
        /// </summary>
        /// <param name="userId">ID of the user associated with the action log.</param>
        /// <param name="actionTypeID">ID of the action type performed.</param>
        /// <param name="datetime">Date and time of the action.</param>
        public ActionLog(Guid userId, int actionTypeID, DateTime dateTimeStamp)
        {
            this.Id = Guid.NewGuid();
            this.userId = userId;
            this.actionTypeID = actionTypeID;
            this.dateTimeStamp = dateTimeStamp;
        }

    }
}
