using System.Security.Claims;

namespace eCommerce.Application.Services.Interfaces.Authentication
{
    public interface ITokenManager
    {
        /// <summary>
        /// Extracts the claims from a JWT token.
        /// </summary>
        /// <param name="token">The JWT token from which claims will be extracted.</param>
        /// <returns>A list of claims associated with the token.</returns>
        List<Claim> GetUserClaimsFromToken(string token);

        /// <summary>
        /// Validates the provided refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token to validate.</param>
        /// <returns>True if the refresh token is valid, otherwise false.</returns>
        Task<bool> ValidateRefreshToken(string refreshToken);

        /// <summary>
        /// Retrieves the user ID associated with the provided refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token to look up the user ID.</param>
        /// <returns>The user ID linked to the refresh token.</returns>
        Task<string> GetUserIdByRefreshToken(string refreshToken);

        /// <summary>
        /// Adds a new refresh token for the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user to associate with the refresh token.</param>
        /// <param name="refreshToken">The refresh token to store.</param>
        /// <returns>An integer indicating the result of the operation (e.g., success or failure).</returns>
        Task<int> AddRefreshToken(string userId, string refreshToken);

        /// <summary>
        /// Updates an existing refresh token for the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user whose refresh token is to be updated.</param>
        /// <param name="refreshToken">The new refresh token to associate with the user.</param>
        /// <returns>An integer indicating the result of the operation (e.g., success or failure).</returns>
        Task<int> UpdateRefreshToken(string userId, string refreshToken);

        /// <summary>
        /// Generates a new JWT token based on the provided claims.
        /// </summary>
        /// <param name="claims">The list of claims to be included in the token.</param>
        /// <returns>A new JWT token.</returns>
        string GenerateToken(List<Claim> claims);

        /// <summary>
        /// Generates a new refresh token.
        /// </summary>
        /// <returns>A new refresh token string.</returns>
        string GetRefreshToken();
    }
}
