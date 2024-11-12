using eCommerce.Domain.Entities.Identity;

namespace eCommerce.Application.Services.Interfaces.Authentication
{
    /// <summary>
    /// Interface for managing user roles in the authentication system.
    /// Provides methods for retrieving user roles and assigning roles to users.
    /// </summary>
    public interface IRoleManager
    {
        /// <summary>
        /// Retrieves the role of a user based on their email.
        /// </summary>
        /// <param name="userEmail">The email address of the user whose role is to be fetched.</param>
        /// <returns>The role of the user or null if no role is found.</returns>
        Task<string?> GetUserRole(string userEmail);

        /// <summary>
        /// Assigns a role to a user.
        /// </summary>
        /// <param name="user">The user to which the role will be assigned.</param>
        /// <param name="roleName">The name of the role to be assigned.</param>
        /// <returns>True if the role was successfully assigned, otherwise false.</returns>
        Task<bool> AddUserToRole(AppUser user, string roleName);
    }
}
