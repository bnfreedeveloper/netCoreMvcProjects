using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netCoreMvcAdo.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        [Column(TypeName = "nvarchar(150)")]
        public string? Email { get; set; }
    }
}
