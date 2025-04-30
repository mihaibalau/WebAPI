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
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        public string Mail { get; set; }

        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = "User";

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public DateOnly BirthDate { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(13, ErrorMessage = "CNP must be exactly 13 characters.")]
        [CnpValidation]
        public string CNP { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        [PhoneNumberValidation]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
