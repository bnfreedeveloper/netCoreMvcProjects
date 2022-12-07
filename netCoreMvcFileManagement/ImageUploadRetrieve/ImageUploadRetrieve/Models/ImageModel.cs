using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageUploadRetrieve.Models
{
    public class ImageModel
    {
       [Key]
       public int ImageId { get; set; }
       [Column(TypeName ="nvarchar(50)")]
       [Required]
       public string? Title { get; set; }
       [Column(TypeName = "nvarchar(100)")]
       [Required]
       public string? ImageName { get; set; }    
    }
}
