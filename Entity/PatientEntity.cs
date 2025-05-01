using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class PatientEntity
    {
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [MaxLength(3)]
        [BloodTypeValidation]
        public string BloodType { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "EmergencyContact must be exactly 10 characters.")]
        public string EmergencyContact { get; set; }

        public string? Allergies { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Weight must be greater than 0.")]
        public double Weight { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Height must be greater than 0.")]
        public int Height { get; set; }

        public virtual UserEntity User { get; set; }
    }
}