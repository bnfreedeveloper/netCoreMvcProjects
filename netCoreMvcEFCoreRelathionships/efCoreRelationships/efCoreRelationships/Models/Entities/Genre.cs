using System.ComponentModel.DataAnnotations.Schema;

namespace efCoreRelationships.Models.Entities
{
    [Table("Genre")]
    public class Genre
    {
        public int Id { get; set; }
        public string? Name { get; set; }    
    }
}
