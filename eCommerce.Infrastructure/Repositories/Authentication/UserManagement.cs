using eCommerce.Application.Services.Interfaces.Authentication;
using eCommerce.Domain.Entities.Identity;
using eCommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace eCommerce.Infrastructure.Repositories.Authentication
{
    /// <summary>
    /// UserManagement class that implements IUserManager.
    /// Provides methods for creating, retrieving, logging in, and removing users.
    /// </summary>
    public class UserManagement(IRoleManager _roleManagement, UserManager<AppUser> _userManager, AppDbContext _dbContext) : IUserManager
    {
        /// <summary>
        /// Creates a new user in the system if the email does not already exist.
        /// </summary>
        /// <param name="user">The user to be created.</param>
        /// <returns>True if the user was created successfully, false if the user already exists.</returns>
        public async Task<bool> CreateUser(AppUser user)
        {
            var _user = await GetUserByEmail(user.Email!);
            if (_user != null) return false; // If user with the same email already exists, return false

            // Create the new user
            return (await _userManager.CreateAsync(user, user.PasswordHash!)).Succeeded;
        }

        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>A list of all users.</returns>
        public async Task<IEnumerable<AppUser>?> GetAllUsers() =>
            await _dbContext.Users.ToListAsync();

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email of the user to be retrieved.</param>
        /// <returns>The user associated with the provided email, or null if no user is found.</returns>
        public async Task<AppUser?> GetUserByEmail(string email) =>
            await _userManager.FindByEmailAsync(email);

        /// <summary>
        /// Retrieves a user by their unique ID.
        /// </summary>
        /// <param name="id">The unique ID of the user to be retrieved.</param>
        /// <returns>The user associated with the provided ID, or null if no user is found.</returns>
        public async Task<AppUser?> GetUserById(string id) =>
            await _userManager.FindByIdAsync(id);

        /// <summary>
        /// Retrieves the claims for a user based on their email.
        /// </summary>
        /// <param name="email">The email of the user whose claims are to be retrieved.</param>
        /// <returns>A list of claims associated with the user.</returns>
        public async Task<List<Claim>> GetUserClaims(string email)
        {
            var _user = await GetUserByEmail(email);
            string? roleName = await _roleManagement.GetUserRole(_user!.Email!);

            // Create a list of claims based on the user's details and role
            List<Claim> claims = new List<Claim>
            {
                new Claim("FullName", _user.FullName),
                new Claim(ClaimTypes.NameIdentifier, _user!.Id),
                new Claim(ClaimTypes.Email, _user!.Email!),
                new Claim(ClaimTypes.Role, roleName!) // Add the role as a claim
            };
            return claims;
        }

        /// <summary>
        /// Authenticates a user by checking their email and password.
        /// </summary>
        /// <param name="user">The user to be authenticated (includes email and password).</param>
        /// <returns>True if the user credentials are valid, false otherwise.</returns>
        public async Task<bool> LoginUser(AppUser user)
        {
            var _user = await GetUserByEmail(user.Email!);
            if (_user is null) return false; // Return false if no user is found

            string? roleName = await _roleManagement.GetUserRole(_user!.Email!);
            if (string.IsNullOrEmpty(roleName)) return false; // If no role is found for the user, return false

            // Check if the provided password matches the stored password
            return await _userManager.CheckPasswordAsync(_user, user.PasswordHash!);
        }

        /// <summary>
        /// Removes a user from the system based on their email address.
        /// </summary>
        /// <param name="email">The email of the user to be removed.</param>
        /// <returns>The number of affected rows (i.e., the number of rows deleted from the database).</returns>
        public async Task<int> RemoveUserByEmail(string email)
        {
            var _user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (_user == null) return 0; // If user not found, return 0

            _dbContext.Users.Remove(_user); // Remove the user from the database
            return await _dbContext.SaveChangesAsync(); // Save changes to the database
        }
    }
}
