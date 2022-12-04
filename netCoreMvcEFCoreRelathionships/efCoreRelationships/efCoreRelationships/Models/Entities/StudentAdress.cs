using System.ComponentModel.DataAnnotations;

namespace efCoreRelationships.Models.Entities
{
    public class StudentAdress
    {
        public int Id { get; set; } 
        [Required]
        public string? Adress { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Country { get; set; }
     
        public int StudentId { get; set; }  
        public Student? Student { get; set; }    
    }
}
