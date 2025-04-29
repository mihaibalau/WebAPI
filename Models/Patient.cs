using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Patient
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }  // Primary Key and FK to Users

        [Required]
        [MaxLength(3)]
        [RegularExpression(@"^(A|B|AB|O)[+-]$", ErrorMessage = "Invalid blood type.")]
        public string BloodType { get; set; }

        [Required]
        [MaxLength(20)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Emergency contact must be 10 digits.")]
        public string EmergencyContact { get; set; }

        [MaxLength(255)]
        public string? Allergies { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Weight must be greater than 0.")]
        public double Weight { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Height must be greater than 0.")]
        public int Height { get; set; }

        // Navigation property
        public virtual User User { get; set; }
    }
}
