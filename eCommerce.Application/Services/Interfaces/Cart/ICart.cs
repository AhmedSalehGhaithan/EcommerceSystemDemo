using eCommerce.Domain.Entities.Cart;

namespace eCommerce.Application.Services.Interfaces.Cart
{
    public interface ICart
    {
        Task<int> SaveCheckoutHistory(IEnumerable<Achieve> checkout);
    }
}
