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
        public int notificationId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int userId { get; set; }

        [Required]
        public DateTime deliveryDateTime { get; set; }

        [Required]
        [MaxLength(256)]
        public string message { get; set; }

        // Navigation property
        public virtual UserEntity user { get; set; }
    }
}
