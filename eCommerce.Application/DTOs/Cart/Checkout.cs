using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Cart
{
    /// <summary>
    /// DTO class representing the checkout process for an e-commerce cart.
    /// This contains the necessary information to proceed with a checkout, 
    /// including the selected payment method and the list of items in the cart.
    /// </summary>
    public class Checkout
    {
        /// <summary>
        /// Gets or sets the unique identifier for the selected payment method.
        /// This is a required field to complete the checkout process.
        /// </summary>
        [Required]
        public required Guid PaymentMethodId { get; set; }

        /// <summary>
        /// Gets or sets the collection of cart items that are being processed for checkout.
        /// This is a required field to proceed with the checkout.
        /// </summary>
        [Required]
        public required IEnumerable<ProcessCart> Carts { get; set; }
    }
}
