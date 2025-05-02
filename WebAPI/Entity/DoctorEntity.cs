using Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    /// <summary>
    /// Represents a doctor in the system.
    /// </summary>
    public class DoctorEntity
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        [Required]
        [Range(0.0, 5.0, ErrorMessage = "Doctor rating must be between 0.0 and 5.0.")]
        public double DoctorRating { get; set; } = 0.0f;

        [Required]
        [MaxLength(50)]
        public string LicenseNumber { get; set; }

        // Navigation properties
        public virtual UserEntity User { get; set; }
        public virtual DepartmentEntity Department { get; set; }
    }
}
