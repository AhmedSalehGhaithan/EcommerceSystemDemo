using eCommerce.Application.Services.Interfaces.Authentication;
using eCommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Infrastructure.Repositories.Authentication
{
    /// <summary>
    /// RoleManagement class that implements the IRoleManager interface.
    /// Provides methods to manage user roles, such as adding a user to a role and getting a user's role.
    /// </summary>
    public class RoleManagement(UserManager<AppUser> _userManager) : IRoleManager
    {
        /// <summary>
        /// Adds the user to the specified role.
        /// </summary>
        /// <param name="user">The user to add to the role.</param>
        /// <param name="roleName">The name of the role to add the user to.</param>
        /// <returns>True if the role was successfully added, otherwise false.</returns>
        public async Task<bool> AddUserToRole(AppUser user, string roleName)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }

        /// <summary>
        /// Retrieves the role of a user based on their email address.
        /// </summary>
        /// <param name="userEmail">The email address of the user whose role is to be retrieved.</param>
        /// <returns>The role name as a string, or null if no role is found.</returns>
        public async Task<string?> GetUserRole(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault(); // Return the first role or null if no roles are assigned
        }
    }
}
