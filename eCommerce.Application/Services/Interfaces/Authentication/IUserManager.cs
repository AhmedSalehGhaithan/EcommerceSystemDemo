using eCommerce.Domain.Entities.Identity;
using System.Security.Claims;

namespace eCommerce.Application.Services.Interfaces.Authentication
{
    /// <summary>
    /// Interface for managing user operations such as creation, login, retrieval, and removal.
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Creates a new user in the system.
        /// </summary>
        /// <param name="user">The user to be created.</param>
        /// <returns>True if the user was successfully created, otherwise false.</returns>
        Task<bool> CreateUser(AppUser user);

        /// <summary>
        /// Logs in a user by validating their credentials.
        /// </summary>
        /// <param name="user">The user attempting to log in.</param>
        /// <returns>True if login is successful, otherwise false.</returns>
        Task<bool> LoginUser(AppUser user);

        /// <summary>
        /// Retrieves a user based on their email address.
        /// </summary>
        /// <param name="email">The email of the user to be fetched.</param>
        /// <returns>The user associated with the given email, or null if not found.</returns>
        Task<AppUser?> GetUserByEmail(string email);

        /// <summary>
        /// Retrieves a user based on their user ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user to retrieve.</param>
        /// <returns>The user associated with the given ID, or null if not found.</returns>
        Task<AppUser?> GetUserById(string id);

        /// <summary>
        /// Retrieves all users in the system.
        /// </summary>
        /// <returns>A list of all users, or null if no users exist.</returns>
        Task<IEnumerable<AppUser>?> GetAllUsers();

        /// <summary>
        /// Removes a user from the system based on their email.
        /// </summary>
        /// <param name="email">The email of the user to be removed.</param>
        /// <returns>An integer indicating the result (e.g., number of records affected).</returns>
        Task<int> RemoveUserByEmail(string email);

        /// <summary>
        /// Retrieves the claims associated with a user by their email.
        /// </summary>
        /// <param name="email">The email of the user whose claims are to be retrieved.</param>
        /// <returns>A list of claims associated with the user.</returns>
        Task<List<Claim>> GetUserClaims(string email);
    }
}
