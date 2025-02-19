using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Models
{
    public class AppUser:IdentityUser
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
    }
}
