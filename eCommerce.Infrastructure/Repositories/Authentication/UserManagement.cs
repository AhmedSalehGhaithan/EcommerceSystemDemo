using eCommerce.Application.Services.Interfaces.Authentication;
using eCommerce.Domain.Entities.Identity;
using eCommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace eCommerce.Infrastructure.Repositories.Authentication
{
    public class UserManagement(IRoleManager _roleManagement , UserManager<AppUser> _userManager , AppDbContext _dbContext) : IUserManager
    {
        public async Task<bool> CreateUser(AppUser user)
        {
            var _user = await GetUserByEmail(user.Email!);
            if (_user != null) return false;

            return (await _userManager.CreateAsync(user, user.PasswordHash!)).Succeeded;
        }

        public async Task<IEnumerable<AppUser>?> GetAllUsers() =>
            await _dbContext.Users.ToListAsync();

        public async Task<AppUser?> GetUserByEmail(string email) => 
            await _userManager.FindByEmailAsync(email);

        public async Task<AppUser?> GetUserById(string id) =>
            await _userManager.FindByIdAsync(id);

        public async Task<List<Claim>> GetUserClaims(string email)
        {
            var _user = await GetUserByEmail(email);
            string? roleName = await _roleManagement.GetUserRole(_user!.Email!);

            List<Claim> claims =
            [
                new Claim("FullName", _user.FullName),
                new Claim(ClaimTypes.NameIdentifier, _user!.Id),
                new Claim(ClaimTypes.Email, _user!.Email!),
                new Claim(ClaimTypes.Role, roleName!)
            ];

            return claims;
        }

        public async Task<bool> LoginUser(AppUser user)
        {
            var _user = await GetUserByEmail(user.Email!);
            if (_user is null) return false;

            string? roleName = await  _roleManagement.GetUserRole(_user!.Email!);
            if(string.IsNullOrEmpty(roleName)) return false;

            return await _userManager.CheckPasswordAsync(_user, user.PasswordHash!);
        }

        public async Task<int> RemoveUserByEmail(string email)
        {
            var _user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            _dbContext.Users.Remove(_user!);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
