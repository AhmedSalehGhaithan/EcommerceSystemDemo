using eCommerce.Domain.Entities.Identity;

namespace eCommerce.Application.Services.Interfaces.Authentication
{
    public interface IRoleManager
    {
        Task<string?> GetUserRole(string userEmail);
        Task<bool> AddUserToRole(AppUser user, string roleName);
    } 
}
