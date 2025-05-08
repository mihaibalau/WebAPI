using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class LogEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int logId { get; set; }

        [ForeignKey("User")]
        public int? userId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [ActionTypeValidation]
        public string actionType { get; set; }

        [Required]
        public DateTime timestamp { get; set; } = DateTime.Now;

        public virtual UserEntity user { get; set; }
    }
}