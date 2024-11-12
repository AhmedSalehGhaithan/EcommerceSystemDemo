using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodsController(IPaymentMethodService _paymentMethodService) : ControllerBase
    {
        [HttpGet("payment-methods")]
        public async Task<ActionResult<IEnumerable<GetPaymentMethod>>> GetPaymentMethods()
        {
            var methods = await _paymentMethodService.GetPaymentMethodsAsync();
            if (!methods.Any()) return NotFound();
            return Ok(methods);
        }
    }
}
