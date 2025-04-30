using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Department
    {
        [Key]  // Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Auto-increment (IDENTITY(1,1))
        public int Id { get; set; }

        [Required]  // NOT NULL
        [MaxLength(100)]  // Limit the name length to 100 characters
        public string Name { get; set; }
    }
}
