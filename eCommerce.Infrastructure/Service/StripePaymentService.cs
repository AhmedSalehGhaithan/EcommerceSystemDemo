using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.DTOs.Response;
using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Domain.Entities;
using Stripe.Checkout;

namespace eCommerce.Infrastructure.Service
{
    public class StripePaymentService : IPaymentService
    {
        public async Task<ServiceResponse> Pay(decimal totalAmount, IEnumerable<Product> cartProduct, IEnumerable<ProcessCart> carts)
        {
            try
            {
                var lineItems = new List<SessionLineItemOptions>();
                foreach (var product in cartProduct)
                {
                    var pQuantity = carts.FirstOrDefault(_ => _.ProductId == product.Id);
                    lineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = product.Name,
                                Description = product.Description,
                            },
                            UnitAmount = (long)(product.Price * 100),
                        },
                        Quantity = pQuantity!.Quantity,
                    });
                }

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = ["usd"],
                    LineItems = lineItems,
                    Mode = "payment",
                    SuccessUrl = "https:localhost:7025/payment-success",
                    CancelUrl = "https:localhost:7025/payment-cancel",

                };

                var service = new SessionService();
                Session session = await service.CreateAsync(options);

                return new ServiceResponse(true, session.Url);

            }
            catch(Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
