using Microsoft.AspNetCore.Identity;

namespace eCommerce.Domain.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
