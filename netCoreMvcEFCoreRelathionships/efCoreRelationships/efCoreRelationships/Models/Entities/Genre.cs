using System.ComponentModel.DataAnnotations.Schema;

namespace efCoreRelationships.Models.Entities
{
    [Table("Genre")]
    public class Genre
    {
        public Genre()
        {
            Books = new HashSet<Book>();
        }
        public int Id { get; set; }
        public string? Name { get; set; }  
        
        public ICollection<Book> Books { get; set; }
    }
}
