using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class DepartmentEntity
    {
        [Key]  // Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Auto-increment (IDENTITY(1,1))
        public int id { get; set; }

        [Required]  // NOT NULL
        [MaxLength(100)]  // Limit the name length to 100 characters
        public string name { get; set; }
    }
}
