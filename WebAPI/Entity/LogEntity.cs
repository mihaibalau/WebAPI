using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class LogEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogId { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [ActionTypeValidation]
        public string ActionType { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.Now;

        public virtual UserEntity User { get; set; }
    }
}