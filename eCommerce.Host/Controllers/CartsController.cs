using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController(ICartService _cartService) : ControllerBase
    {
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(Checkout checkout)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _cartService.Checkout(checkout);
            return result.Flag ? Ok(result) : BadRequest(result);
        }

        [HttpPost("SaveCheckout")]
        public async Task<IActionResult> SaveCheckout(IEnumerable<CreateAchieve> achieve)
        {
            var result = await _cartService.SaveCheckoutHistory(achieve);
            return result.Flag ? Ok(result) : BadRequest(result);
        }
    }
}
