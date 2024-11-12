using eCommerce.Application.DTOs.Identity;
using eCommerce.Application.Services.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Host.Controllers
{
    /// <summary>
    /// Controller for handling user authentication operations like registration, login, and token refresh.
    /// </summary>
    /// <param name="_authenticationService">The authentication service used for user operations.</param>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService _authenticationService) : ControllerBase
    {
        /// <summary>
        /// Endpoint to create a new user.
        /// </summary>
        /// <param name="user">The user data for account creation.</param>
        /// <returns>Returns an OK response with result if successful, or a BadRequest with error message.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(CreateUser user)
        {
            var result = await _authenticationService.CreateUser(user);
            return result.Flag ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Endpoint to authenticate a user and generate a JWT and refresh token.
        /// </summary>
        /// <param name="user">The login credentials of the user.</param>
        /// <returns>Returns an OK response with login result if successful, or a BadRequest with error message.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUser user)
        {
            var result = await _authenticationService.LoginUser(user);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Endpoint to refresh the authentication token using a valid refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token used to obtain a new JWT and refresh token.</param>
        /// <returns>Returns an OK response with new tokens if successful, or a BadRequest with error message.</returns>
        [HttpGet("refreshToken/{refreshToken}")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var result = await _authenticationService.ReviveToken(refreshToken);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
