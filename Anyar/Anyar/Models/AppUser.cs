using Microsoft.AspNetCore.Identity;

namespace Anyar.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
