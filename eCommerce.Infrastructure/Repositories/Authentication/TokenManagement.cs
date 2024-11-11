using eCommerce.Application.Services.Interfaces.Authentication;
using eCommerce.Domain.Entities.Identity;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace eCommerce.Infrastructure.Repositories.Authentication
{
    public class TokenManagement(AppDbContext _context , IConfiguration _config) : ITokenManager
    {
        public async Task<int> AddRefreshToken(string userId, string refreshToken)
        {
            _context.refreshTokens.Add(new RefreshToken
            {
                UserId = userId,
                Token = refreshToken
            });

            return await _context.SaveChangesAsync();
        }

        public string GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!));
            var credential = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(2);

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                signingCredentials: credential,
                expires: expiration
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public string GetRefreshToken()
        {
            const int byteSize = 64;
            byte[] randomByte = new byte[byteSize];

            using(RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomByte);
            }

            return Convert.ToBase64String(randomByte);

        }

        public List<Claim> GetUserClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            if (jwtToken is not null) return jwtToken.Claims.ToList();
            else return [];
        }

        public async Task<string> GetUserIdByRefreshToken(string refreshToken) =>
            (await _context.refreshTokens.FirstOrDefaultAsync(_ => _.Token == refreshToken))!.UserId;

        public async Task<int> UpdateRefreshToken(string userId, string refreshToken)
        {
            var data = new RefreshToken
            {
                UserId = userId,
                Token = refreshToken
            };
            var user = await _context.refreshTokens.FirstOrDefaultAsync(_ => _.Token == refreshToken);
            if (user is null) return -1;
            user.Token = refreshToken;
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateRefreshToken(string refreshToken)
        {
            var _user = await _context.refreshTokens.FirstOrDefaultAsync(_ => _.Token == refreshToken);
            return _user != null;
        }
    }
}
