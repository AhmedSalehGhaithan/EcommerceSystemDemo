using System;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Product
{
    /// <summary>
    /// Base class for product data transfer objects.
    /// </summary>
    public class ProductBase
    {
        /// <summary>
        /// Gets or sets the name of the product.
        /// Required and must be between 1 and 100 characters.
        /// </summary>
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Product name must be between 1 and 100 characters.")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// Required and must be between 1 and 500 characters.
        /// </summary>
        [Required(ErrorMessage = "Product description is required.")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Product description must be between 1 and 500 characters.")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the URL of the product image.
        /// Required and must be a valid URL.
        /// </summary>
        [Required(ErrorMessage = "Product image URL is required.")]
        public string? Image { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// Required and must be greater than zero.
        /// </summary>
        [Required(ErrorMessage = "Product price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Product price must be greater than zero.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the available quantity of the product.
        /// Required and must be greater than or equal to zero.
        /// </summary>
        [Required(ErrorMessage = "Product quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Product quantity must be greater than or equal to zero.")]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the product's category.
        /// Required.
        /// </summary>
        [Required(ErrorMessage = "Category ID is required.")]
        public Guid CategoryId { get; set; }
    }
}
