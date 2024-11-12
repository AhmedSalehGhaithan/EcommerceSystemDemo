using eCommerce.Application.Services.Interfaces.Authentication;
using eCommerce.Domain.Entities.Identity;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace eCommerce.Infrastructure.Repositories.Authentication
{
    /// <summary>
    /// Manages operations related to JWT and refresh tokens, including generating and validating tokens.
    /// </summary>
    public class TokenManagement(AppDbContext _context, IConfiguration _config) : ITokenManager
    {
        /// <summary>
        /// Adds a new refresh token to the database for a specified user.
        /// </summary>
        /// <param name="userId">The ID of the user to associate with the refresh token.</param>
        /// <param name="refreshToken">The refresh token to be added.</param>
        /// <returns>The number of state entries written to the database.</returns>
        public async Task<int> AddRefreshToken(string userId, string refreshToken)
        {
            _context.refreshTokens.Add(new RefreshToken
            {
                UserId = userId,
                Token = refreshToken
            });
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Generates a JWT token with the specified claims.
        /// </summary>
        /// <param name="claims">The claims to include in the token.</param>
        /// <returns>A string representing the generated JWT token.</returns>
        public string GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(2); // Token expires in 2 hours

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                signingCredentials: credential,
                expires: expiration
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Generates a new random refresh token.
        /// </summary>
        /// <returns>A newly generated refresh token as a base64-encoded string.</returns>
        public string GetRefreshToken()
        {
            const int byteSize = 64;
            byte[] randomByte = new byte[byteSize];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomByte);
            }
            string token = Convert.ToBase64String(randomByte);
            return WebUtility.UrlEncode(token); // Encodes to make it URL-safe
        }

        /// <summary>
        /// Extracts the claims from a given JWT token.
        /// </summary>
        /// <param name="token">The JWT token to extract claims from.</param>
        /// <returns>A list of claims associated with the provided token.</returns>
        public List<Claim> GetUserClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            if (jwtToken is not null)
                return jwtToken.Claims.ToList();
            else
                return new List<Claim>(); // Return an empty list if token is invalid
        }

        /// <summary>
        /// Retrieves the user ID associated with a given refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token whose associated user ID is to be retrieved.</param>
        /// <returns>The user ID associated with the refresh token.</returns>
        public async Task<string> GetUserIdByRefreshToken(string refreshToken) =>
            (await _context.refreshTokens.FirstOrDefaultAsync(_ => _.Token == refreshToken))!.UserId;

        /// <summary>
        /// Updates an existing refresh token for a specified user.
        /// </summary>
        /// <param name="userId">The user ID to update the refresh token for.</param>
        /// <param name="refreshToken">The new refresh token to set.</param>
        /// <returns>The number of state entries written to the database (or -1 if the refresh token doesn't exist).</returns>
        public async Task<int> UpdateRefreshToken(string userId, string refreshToken)
        {
            var existingToken = await _context.refreshTokens.FirstOrDefaultAsync(_ => _.Token == refreshToken);
            if (existingToken == null) return -1; // Return -1 if the refresh token does not exist

            existingToken.Token = refreshToken; // Update the refresh token
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Validates if a given refresh token exists in the database.
        /// </summary>
        /// <param name="refreshToken">The refresh token to validate.</param>
        /// <returns>True if the refresh token exists, otherwise false.</returns>
        public async Task<bool> ValidateRefreshToken(string refreshToken)
        {
            var _user = await _context.refreshTokens.FirstOrDefaultAsync(_ => _.Token == refreshToken);
            return _user != null; // Return true if the refresh token is found
        }
    }
}
