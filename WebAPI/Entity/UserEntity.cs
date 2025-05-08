using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }

        [Required]
        [MaxLength(50)]
        public string username { get; set; }

        [Required]
        [MaxLength(255)]
        public string password { get; set; }

        [Required]
        [MaxLength(100)]
        public string mail { get; set; }

        [Required]
        [MaxLength(50)]
        public string role { get; set; } = "User";

        [Required]
        [MaxLength(100)]
        public string name { get; set; }

        [Required]
        public DateOnly birthDate { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(13, ErrorMessage = "CNP must be exactly 13 characters.")]
        [CnpValidation]
        public string cnp { get; set; }

        [MaxLength(255)]
        public string? address { get; set; }

        [PhoneNumberValidation]
        [MaxLength(20)]
        public string? phoneNumber { get; set; }

        [Required]
        public DateTime registrationDate { get; set; } = DateTime.Now;
    }
}
