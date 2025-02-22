using System.ComponentModel.DataAnnotations;

namespace AdminPanel.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Don't null!"),StringLength(15)]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Don't null!"), StringLength(20)]
        public string? FullName { get; set; }
        public string? Email { get; set; }
        [Required(ErrorMessage = "Don't null!"), StringLength(100), DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Don't null!"), StringLength(100), DataType(DataType.Password),Compare(nameof(Password),ErrorMessage="Password not correct!")]
        public string? ConfirmPassword { get; set; }
      
    }
}
