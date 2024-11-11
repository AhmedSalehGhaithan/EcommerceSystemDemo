using eCommerce.Domain.Entities.Identity;
using System.Security.Claims;

namespace eCommerce.Application.Services.Interfaces.Authentication
{
    public interface IUserManager
    {
        Task<bool> CreateUser(AppUser user);
        Task<bool> LoginUser(AppUser user);
        Task<AppUser?> GetUserByEmail(string email);
        Task<AppUser?> GetUserById(string id);
        Task<IEnumerable<AppUser>?> GetAllUsers();
        Task<int> RemoveUserByEmail(string email);
        Task<List<Claim>> GetUserClaims(string email);
    } 
}
