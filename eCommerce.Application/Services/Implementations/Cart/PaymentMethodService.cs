using AutoMapper;
using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces.Cart;

namespace eCommerce.Application.Services.Implementations.Cart
{
    public class PaymentMethodService(IPaymentMethod _paymentMethod, IMapper _mapper) : IPaymentMethodService
    {
        public async Task<IEnumerable<GetPaymentMethod>> GetPaymentMethodsAsync()
        {
            var methods = await _paymentMethod.GetPaymentMethodAsync();
            if (!methods.Any()) return [];
            return _mapper.Map<IEnumerable<GetPaymentMethod>>(methods);
        }
    }
}
