using System.ComponentModel.DataAnnotations;

namespace University.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName { get; set; } = string.Empty;
    }
}
