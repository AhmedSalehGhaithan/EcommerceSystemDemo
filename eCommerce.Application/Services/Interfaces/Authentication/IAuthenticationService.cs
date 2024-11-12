using eCommerce.Application.DTOs.Identity;
using eCommerce.Application.DTOs.Response;

namespace eCommerce.Application.Services.Interfaces.Authentication
{
    /// <summary>
    /// Defines the contract for authentication-related services.
    /// Provides methods for user creation, login, and token refresh.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Creates a new user in the system.
        /// </summary>
        /// <param name="user">The user details for account creation.</param>
        /// <returns>A ServiceResponse indicating success or failure.</returns>
        Task<ServiceResponse> CreateUser(CreateUser user);

        /// <summary>
        /// Logs in an existing user by verifying credentials.
        /// </summary>
        /// <param name="user">The login details including username/email and password.</param>
        /// <returns>A LoginResponse containing the user authentication result and token.</returns>
        Task<LoginResponse> LoginUser(LoginUser user);

        /// <summary>
        /// Revives a user's session by refreshing their token using a valid refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token used to get a new access token.</param>
        /// <returns>A LoginResponse with the new authentication tokens.</returns>
        Task<LoginResponse> ReviveToken(string refreshToken);
    }
}
