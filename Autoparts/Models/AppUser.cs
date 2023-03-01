using Microsoft.AspNetCore.Identity;

namespace Autoparts.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
