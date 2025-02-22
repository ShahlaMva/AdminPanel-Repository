using System.ComponentModel.DataAnnotations;

namespace AdminPanel.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string? UserNameOrEmail { get; set; }
        [Required]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
