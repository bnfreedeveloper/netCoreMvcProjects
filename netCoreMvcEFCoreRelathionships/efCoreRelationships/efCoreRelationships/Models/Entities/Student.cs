using System.ComponentModel.DataAnnotations;

namespace efCoreRelationships.Models.Entities
{
    public class Student
    {
        public int Id { get; set; } 
        [Required]
        public string? Name { get; set; }
        public int PhoneNumber { get; set; }    
        public StudentAdress? Location { get; set; } 
    }
}
