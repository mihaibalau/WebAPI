using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogId { get; set; }

        public int? UserId { get; set; }  // Nullable for anonymous actions

        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^(LOGIN|LOGOUT|UPDATE_PROFILE|CHANGE_PASSWORD|DELETE_ACCOUNT|CREATE_ACCOUNT)$",
            ErrorMessage = "Invalid action type.")]
        public string ActionType { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.Now;

        // Navigation property
        public virtual User? User { get; set; }
    }
}
