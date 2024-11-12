using AutoMapper;
using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.DTOs.Response;
using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Entities.Cart;
using eCommerce.Domain.Interfaces;

namespace eCommerce.Application.Services.Implementations.Cart
{

    public class CartService(ICart _cart , IMapper _mapper, IGeneric<Product> _productInterface , IPaymentMethodService _paymentMethodService, IPaymentService _paymentService) : ICartService
    {
        public async Task<ServiceResponse> Checkout(Checkout checkout)
        {
            var (product, totalAmount) = await GetCartTotalAmount(checkout.Carts);
            var paymentMethod = await _paymentMethodService.GetPaymentMethodsAsync();
            if (checkout.PaymentMethodId == paymentMethod.FirstOrDefault()!.Id)
                return await _paymentService.Pay(totalAmount, product, checkout.Carts);
           
            else 
                return new ServiceResponse(false, " Invalid payment method");
        }

        public async Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateAchieve> checkout)
        {
            var mapData = _mapper.Map<IEnumerable<Achieve>>(checkout);
            var result = await _cart.SaveCheckoutHistory(mapData);
            return result > 0 ? new ServiceResponse(true, "Checkout Achieved") : new ServiceResponse(false, "Error occurred in saving");
        }

        private async Task<(IEnumerable<Product>,decimal)> GetCartTotalAmount(IEnumerable<ProcessCart> carts)
        {
            if(!carts.Any()) return (Enumerable.Empty<Product>(),0);
            var products = await _productInterface.GetAllAsync();
            if(!products.Any()) return (Enumerable.Empty<Product>(), 0);

            var cartProducts = carts
                .Select(cartItem => products.FirstOrDefault(p => p.Id == cartItem.ProductId))
                .Where(product => product != null).ToList();

            var totalAmounts = carts
                .Where(cartItem => cartProducts.Any(p => p.Id == cartItem.ProductId))
                .Sum(cartItem => cartItem.Quantity * cartProducts.First(p => p.Id == cartItem.ProductId)!.Price);

            return (cartProducts!, totalAmounts);
        }
    }
}
