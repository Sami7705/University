using System.ComponentModel.DataAnnotations;

namespace University.ViewModels
{
public class RegisterUserViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
    }
}
