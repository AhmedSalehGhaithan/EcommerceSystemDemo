using eCommerce.Application.DTOs.Cart;

namespace eCommerce.Application.Services.Interfaces.Cart
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<GetPaymentMethod>> GetPaymentMethodsAsync();
    }
}
