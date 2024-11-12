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
    /// <summary>
    /// Handles user authentication and authorization processes, including user creation, login, and token management.
    /// </summary>
    /// <param name="_tokenManager">Token management service for handling JWT and refresh tokens.</param>
    /// <param name="_userManager">User management service for handling user-related operations.</param>
    /// <param name="_roleManager">Role management service for handling user roles.</param>
    /// <param name="_logger">Logging service for error and info logging.</param>
    /// <param name="_mapper">AutoMapper instance for object mapping.</param>
    /// <param name="_createUserValidator">Validator for user creation inputs.</param>
    /// <param name="_loginUserValidator">Validator for login user inputs.</param>
    /// <param name="_validationService">Service to perform validation operations.</param>
    public class AuthenticationService
        (ITokenManager _tokenManager, IUserManager _userManager,
         IRoleManager _roleManager, IAppLogger<AuthenticationService> _logger,
         IMapper _mapper, IValidator<CreateUser> _createUserValidator,
         IValidator<LoginUser> _loginUserValidator, IValidationService _validationService) : IAuthenticationService
    {
        /// <summary>
        /// Creates a new user, validates inputs, and assigns roles.
        /// </summary>
        /// <param name="user">The user data for creation.</param>
        /// <returns>A service response with success or failure message.</returns>
        public async Task<ServiceResponse> CreateUser(CreateUser user)
        {
            var validationResult = await _validationService.ValidateAsync(user, _createUserValidator);
            if (!validationResult.Flag) return validationResult;

            var mapModel = _mapper.Map<AppUser>(user);
            mapModel.UserName = user.Email;
            mapModel.PasswordHash = user.Password;

            var result = await _userManager.CreateUser(mapModel);
            if (!result)
                return new ServiceResponse { Message = "Email address might be used or unknown error occurred." };

            var _user = await _userManager.GetUserByEmail(user.Email);
            var users = await _userManager.GetAllUsers();
            var assignedResult = await _roleManager.AddUserToRole(_user!, users!.Count() > 1 ? "User" : "Admin");

            if (!assignedResult)
            {
                // remove user
                int removeResult = await _userManager.RemoveUserByEmail(_user!.Email!);
                if (removeResult <= 0)
                {
                    _logger.LogError(new Exception($"User with email as {_user.Email} failed to be removed as a result of role assigning issue."),
                                                    "User could not be assigned to role.");
                    return new ServiceResponse { Message = "Error occurred in account creation." };
                }
            }
            return new ServiceResponse { Flag = true, Message = "Account created successfully" };
        }

        /// <summary>
        /// Authenticates a user, validates credentials, and generates tokens.
        /// </summary>
        /// <param name="user">The login data for the user.</param>
        /// <returns>A login response with the JWT and refresh tokens or an error message.</returns>
        public async Task<LoginResponse> LoginUser(LoginUser user)
        {
            var validationResult = await _validationService.ValidateAsync(user, _loginUserValidator);
            if (!validationResult.Flag) return new LoginResponse(Message: validationResult.Message);

            var mapModel = _mapper.Map<AppUser>(user);
            mapModel.PasswordHash = user.Password;

            bool loginResult = await _userManager.LoginUser(mapModel);
            if (!loginResult) return new LoginResponse(Message: "Email not found or invalid credentials");

            var _user = await _userManager.GetUserByEmail(user.Email);
            var claims = await _userManager.GetUserClaims(user.Email);
            string jwtToken = _tokenManager.GenerateToken(claims);
            string refreshToken = _tokenManager.GetRefreshToken();

            int saveTokenResult = 0;
            bool userTokenCheck = await _tokenManager.ValidateRefreshToken(refreshToken);
            if (userTokenCheck)
                saveTokenResult = await _tokenManager.UpdateRefreshToken(_user!.Id, refreshToken);
            else
                saveTokenResult = await _tokenManager.AddRefreshToken(_user!.Id, refreshToken);

            return saveTokenResult <= 0 ? new LoginResponse(Message: "Internal error occurred while authenticating") :
                new LoginResponse(Success: true, Token: jwtToken, RefreshToken: refreshToken);
        }

        /// <summary>
        /// Revives a user's authentication token using a valid refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token used to obtain new tokens.</param>
        /// <returns>A login response with new JWT and refresh tokens, or an error message.</returns>
        public async Task<LoginResponse> ReviveToken(string refreshToken)
        {
            //Method validates the refresh token to ensure it is still valid
            var validationToken = await _tokenManager.ValidateRefreshToken(refreshToken);
            if (!validationToken) return new LoginResponse(Message: "Invalid token");

            string userId = await _tokenManager.GetUserIdByRefreshToken(refreshToken);
            AppUser? _user = await _userManager.GetUserById(userId);
            var claims = await _userManager.GetUserClaims(_user!.Email!);

            string newJwtToken = _tokenManager.GenerateToken(claims);
            string newRefreshToken = _tokenManager.GetRefreshToken();
            await _tokenManager.UpdateRefreshToken(userId, newRefreshToken);

            return new LoginResponse(Success: true, Token: newJwtToken, RefreshToken: newRefreshToken);
        }
    }
}
