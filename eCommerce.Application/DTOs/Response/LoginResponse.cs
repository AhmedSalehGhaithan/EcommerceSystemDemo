namespace eCommerce.Application.DTOs.Response
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Success"></param>
    /// <param name="Message"></param>
    /// <param name="Token"></param>
    /// <param name="RefreshToken"></param>
    public record LoginResponse(bool Success = false,string Message = null!,string Token = null!,string RefreshToken = null!);
}
