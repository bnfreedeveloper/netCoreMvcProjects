using System.ComponentModel.DataAnnotations;

namespace authAuthorization.Models.Dtos
{
    public class Registration
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [RegularExpression("^(?=.*[0 - 9])(?=.*[a - z])(?=.*[A - Z])(?=.*[@#$%^&-+=()])(?=\\S+$).{6,}$",ErrorMessage ="must contain at least 1 digit,1 uppercase letter , 1 lowercase letter,1 special character and 6 length minimum")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage ="password must match")]
        public string PasswordConfirm { get; set; }
        public string Role { get; set; }    
    }
}
