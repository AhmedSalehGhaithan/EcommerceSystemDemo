using eCommerce.Domain.Entities.Cart;

namespace eCommerce.Application.Services.Interfaces.Cart
{
    public interface IPaymentMethod
    {
        Task<IEnumerable<PaymentMethod>> GetPaymentMethodAsync();
    }
}
