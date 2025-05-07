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
        public int userId { get; set; }

        [Required]
        [ForeignKey("Department")]
        public int departmentId { get; set; }

        [Required]
        [Range(0.0, 5.0, ErrorMessage = "Doctor rating must be between 0.0 and 5.0.")]
        public double doctorRating { get; set; } = 0.0f;

        [Required]
        [MaxLength(50)]
        public string licenseNumber { get; set; }

        // Navigation properties
        public virtual UserEntity user { get; set; }
        public virtual DepartmentEntity department { get; set; }
    }
}
