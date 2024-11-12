namespace eCommerce.Application.DTOs.Cart
{
    /// <summary>
    /// DTO for creating an achievement in the shopping cart.
    /// Contains necessary information to add a product with specified quantity to a user's cart.
    /// </summary>
    public class CreateAchieve
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product being added to the cart.
        /// </summary>
        public required Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product to be added to the cart.
        /// </summary>
        public required int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user who is adding the product to the cart.
        /// </summary>
        public required Guid UserId { get; set; }
    }
}
