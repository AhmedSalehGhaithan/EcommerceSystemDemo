using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Domain.Entities.Cart;
using eCommerce.Infrastructure.Data;

namespace eCommerce.Infrastructure.Repositories.Cart
{
    public class CartRepo(AppDbContext _context) : ICart
    {
        // CheckoutAchieve
        public async Task<int> SaveCheckoutHistory(IEnumerable<Achieve> checkout)
        {
            _context.CheckoutAchieve.AddRange(checkout);
            return await _context.SaveChangesAsync();
        }
    }
}
