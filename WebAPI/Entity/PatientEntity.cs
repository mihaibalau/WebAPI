using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class PatientEntity
    {
        [Required]
        [ForeignKey("User")]
        public int userId { get; set; }

        [Required]
        [MaxLength(3)]
        [BloodTypeValidation]
        public string bloodType { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "EmergencyContact must be exactly 10 characters.")]
        public string emergencyContact { get; set; }

        public string? allergies { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Weight must be greater than 0.")]
        public double weight { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Height must be greater than 0.")]
        public int height { get; set; }

        public virtual UserEntity user { get; set; }
    }
}