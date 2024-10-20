using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.Domain.Entities
{
    /// <summary>
    /// Represents a product in the eCommerce system.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// Required and must be between 1 and 100 characters.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// Optional and can be up to 500 characters.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the image URL of the product.
        /// Optional.
        /// </summary>
        public string? Image { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// Required and must be greater than or equal to zero.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the available quantity of the product.
        /// Required and must be greater than or equal to zero.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the category to which the product belongs.
        /// This can be null if the product is not categorized.
        /// </summary>
        public Category? Category { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the category associated with the product.
        /// </summary>
        public Guid CategoryId { get; set; }
    }
}
