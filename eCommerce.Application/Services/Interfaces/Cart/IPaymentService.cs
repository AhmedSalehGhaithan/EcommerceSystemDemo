using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.DTOs.Response;
using eCommerce.Domain.Entities;

namespace eCommerce.Application.Services.Interfaces.Cart
{
    public interface IPaymentService
    {
        Task<ServiceResponse> Pay(decimal totalAmount, IEnumerable<Product> cartProduct, IEnumerable<ProcessCart> carts);
    }
}
