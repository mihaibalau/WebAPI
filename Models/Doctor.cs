using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Doctor
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }  // Primary Key and FK to Users

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        [Range(0.0, 5.0)]
        public double DoctorRating { get; set; } = 0.0;

        [Required]
        [MaxLength(50)]
        public string LicenseNumber { get; set; }

        public string? CareerInfo { get; set; }

        // Navigation properties
        public virtual User User { get; set; }

        public virtual Department Department { get; set; }
    }
}
