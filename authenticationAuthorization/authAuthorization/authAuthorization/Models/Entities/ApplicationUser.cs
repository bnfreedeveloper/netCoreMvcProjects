using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace authAuthorization.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? Name { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
