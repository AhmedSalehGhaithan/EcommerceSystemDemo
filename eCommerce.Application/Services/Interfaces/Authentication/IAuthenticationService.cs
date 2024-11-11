using eCommerce.Application.DTOs.Identity;
using eCommerce.Application.DTOs.Response;

namespace eCommerce.Application.Services.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse> CreateUser(CreateUser user);
        Task<LoginResponse> LoginUser(LoginUser user);
        Task<LoginResponse> ReviveToken(string refreshToken);

    }
}
