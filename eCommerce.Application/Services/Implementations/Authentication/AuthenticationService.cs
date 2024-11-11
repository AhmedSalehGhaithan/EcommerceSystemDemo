using AutoMapper;
using eCommerce.Application.DTOs.Identity;
using eCommerce.Application.DTOs.Response;
using eCommerce.Application.Services.Interfaces.Authentication;
using eCommerce.Application.Services.Interfaces.Logging;
using eCommerce.Application.Validations;
using eCommerce.Domain.Entities.Identity;
using FluentValidation;

namespace eCommerce.Application.Services.Implementations.Authentication
{

    public class AuthenticationService
        (ITokenManager _tokenManager,IUserManager _userManager ,
         IRoleManager _roleManager , IAppLogger<AuthenticationService> _logger,
         IMapper _mapper, IValidator<CreateUser> _createUserValidator,
         IValidator<LoginUser> _loginUserValidator,IValidationService _validationService) : IAuthenticationService
    {
        public async Task<ServiceResponse> CreateUser(CreateUser user)
        {
            var validationResult = await _validationService.ValidateAsync(user, _createUserValidator);
            if (!validationResult.Flag) return validationResult;

            var mapModel = _mapper.Map<AppUser>(user);
            mapModel.UserName = user.Email;
            mapModel.PasswordHash = user.Password;

            var result = await _userManager.CreateUser(mapModel);
            if (!result)
                return new ServiceResponse { Message = "Email address might be use or unknown error occurred." };

            var _user = await _userManager.GetUserByEmail(user.Email);
            var users = await _userManager.GetAllUsers();
            var assignedResult = await _roleManager.AddUserToRole(_user!, users!.Count() > 1 ? "User" : "Admin");

            if (!assignedResult)
            {
                // remove user
                int removeResult = await _userManager.RemoveUserByEmail(_user!.Email!);
                if (removeResult <= 0)
                {
                    _logger.LogError(new Exception($"User with email as {_user.Email} failed to be remove as a result of role assigning issue."),
                                                    "User could not be assigned to role.");
                    return new ServiceResponse { Message = "Error occurred in create account." };
                }
            }
            return new ServiceResponse { Flag = true, Message = "Account created successfully" };
        }

        public async Task<LoginResponse> LoginUser(LoginUser user)
        {
            var validationResult = await _validationService.ValidateAsync(user, _loginUserValidator);
            if (!validationResult.Flag) return new LoginResponse(Message:validationResult.Message);

            var mapModel = _mapper.Map<AppUser>(user);
            mapModel.PasswordHash = user.Password;

            bool loginResult = await _userManager.LoginUser(mapModel);
            if (!loginResult) return new LoginResponse(Message: "Email not found or invalid credentials");

            var _user = await _userManager.GetUserByEmail(user.Email);
            var claims = await _userManager.GetUserClaims(user.Email);

            string jwtToken = _tokenManager.GenerateToken(claims);
            string refreshToken = _tokenManager.GetRefreshToken();

            var saveTokenResult = await _tokenManager.AddRefreshToken(_user!.Id, refreshToken);
            return saveTokenResult <= 0 ? new LoginResponse(Message: "Internal error occurred while authenticating") :
                new LoginResponse(Success:true, Token:jwtToken, RefreshToken:refreshToken);

        }

        public async Task<LoginResponse> ReviveToken(string refreshToken)
        {
            var validationToken = await _tokenManager.ValidateRefreshToken(refreshToken);
            if (!validationToken) return new LoginResponse(Message: "Invalid token");

            string userId = await _tokenManager.GetUserIdByRefreshToken(refreshToken);
            AppUser? _user = await _userManager.GetUserById(userId);
            var claims = await _userManager.GetUserClaims(_user!.Email!);

            string newJwtToken = _tokenManager.GenerateToken(claims);
            string newRefreshToken = _tokenManager.GetRefreshToken();
            await _tokenManager.UpdateRefreshToken(userId,newRefreshToken);

            return new LoginResponse(Success:true, Token:newJwtToken, RefreshToken:newRefreshToken);

        }
    }
}
