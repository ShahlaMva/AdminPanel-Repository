using AdminPanel.Models;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.ViewModels
{
    public class UserVM
    {
        public IEnumerable<IdentityUser> users { get; set; }
        public IEnumerable<IdentityRole> roles { get; set; }
    }
}
