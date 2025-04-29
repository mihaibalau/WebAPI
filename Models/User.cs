using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class User
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
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(13, ErrorMessage = "CNP must be exactly 13 characters.")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "CNP must contain exactly 13 digits.")]
        public string Cnp { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        [MaxLength(20)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string? PhoneNumber { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [MaxLength(255)]
        public string? AvatarUrl { get; set; }
    }
}
