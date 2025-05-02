using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    /// <summary>
    /// Represents a notification sent to a user.
    /// </summary>
    public class NotificationEntity
    {
        [Key]
        public int NotificationId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public DateTime DeliveryDateTime { get; set; }

        [Required]
        [MaxLength(256)]
        public string Message { get; set; }

        // Navigation property
        public virtual UserEntity User { get; set; }
    }
}
