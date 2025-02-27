using System.ComponentModel.DataAnnotations;

namespace AdminPanel.ViewModels
{
    public class PasswordVM
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Don't null!"), StringLength(15)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Don't null!"), StringLength(15)]
        public string? OldPassword { get; set; }

    }
}
