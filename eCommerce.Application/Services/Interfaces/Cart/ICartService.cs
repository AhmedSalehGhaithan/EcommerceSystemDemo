using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.DTOs.Response;
using eCommerce.Domain.Entities.Cart;

namespace eCommerce.Application.Services.Interfaces.Cart
{
    public interface ICartService
    {
        Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateAchieve> checkout);
        Task<ServiceResponse> Checkout(Checkout checkout);
    }
}
