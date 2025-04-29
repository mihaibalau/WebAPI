using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    /// <summary>
    /// Represents a notification for a user.
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// Gets or sets the unique identifier of the notification.
        /// </summary>
        
        [Key]  // primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Auto increment IDENTITY(1,1)
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the user associated with the notification.
        /// </summary>
        [Required]  // NOT NULL
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the notification is scheduled to be delivered.
        /// </summary>
        [Required]  // NOT NULL
        public DateTime DeliveryDateTime { get; set; }

        /// <summary>
        /// Gets or sets the message content of the notification.
        /// </summary>
        [Required]  // NOT NULL
        [MaxLength(500)]  // MAXIMUM CHARACTERS IS 500
        public string Message { get; set; }

        
    }
}
