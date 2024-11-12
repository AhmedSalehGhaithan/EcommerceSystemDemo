namespace eCommerce.Application.DTOs.Cart
{
    /// <summary>
    /// DTO for representing a payment method in the cart.
    /// Contains details about the payment method's identifier and name.
    /// </summary>
    public class GetPaymentMethod
    {
        /// <summary>
        /// Gets or sets the unique identifier for the payment method.
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the payment method (e.g., Credit Card, PayPal, etc.).
        /// </summary>
        public required string Name { get; set; }
    }
}
