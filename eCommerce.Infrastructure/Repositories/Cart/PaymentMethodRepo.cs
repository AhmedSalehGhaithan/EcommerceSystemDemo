using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Domain.Entities.Cart;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories.Cart
{
    public class PaymentMethodRepo(AppDbContext _context) : IPaymentMethod
    {
        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodAsync() =>
            await _context.PaymentMethods.AsNoTracking().ToListAsync();
    }
}
