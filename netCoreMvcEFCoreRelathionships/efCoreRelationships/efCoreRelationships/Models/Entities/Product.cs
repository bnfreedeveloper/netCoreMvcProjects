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

        [Column("Category_Id")]
        public int? CategoryId { get; set; }  
        
        [ForeignKey("CategoryId")]
        public Categorie? Categorie { get; set; }

    }
}
