using System.ComponentModel.DataAnnotations;

namespace authAuthorization.Models.Dtos
{
    public class Login
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
