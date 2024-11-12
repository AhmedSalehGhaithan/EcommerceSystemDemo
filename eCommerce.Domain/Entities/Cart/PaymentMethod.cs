using System.ComponentModel.DataAnnotations;

namespace eCommerce.Domain.Entities.Cart
{
    public class PaymentMethod
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
    }
}
