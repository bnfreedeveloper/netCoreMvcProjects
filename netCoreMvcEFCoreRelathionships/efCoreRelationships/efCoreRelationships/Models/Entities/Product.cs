using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace efCoreRelationships.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Column(TypeName ="nvarchar(100)")]
        [MinLength(5)]
        [Required]
        public string? Name { get; set; }

        [Column(TypeName ="decimal(5,3)")]
        public decimal? Price { get; set; }

        // i decided to make the fk nullable just for ex
        public int? categId { get; set; }

        [ForeignKey("categId")]
        public Categorie? Categ { get; set; }
        
        [NotMapped]
        public string? CategoryName { get; set; }    
       
    }
}
