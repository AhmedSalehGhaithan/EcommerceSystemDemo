using eCommerce.Application.Services.Interfaces.Authentication;
using eCommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Infrastructure.Repositories.Authentication
{
    public class RoleManagement(UserManager<AppUser> _userManager) : IRoleManager
    {
        public async Task<bool> AddUserToRole(AppUser user, string roleName) =>
            (await _userManager.AddToRoleAsync(user, roleName)).Succeeded;

        public async Task<string?> GetUserRole(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            return (await _userManager.GetRolesAsync(user!)).FirstOrDefault();
        }
    }
}
