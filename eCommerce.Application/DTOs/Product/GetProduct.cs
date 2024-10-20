using eCommerce.Application.DTOs.Category;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Product
{
    /// <summary>
    /// Data transfer object for retrieving product details.
    /// Inherits from ProductBase.
    /// </summary>
    public class GetProduct : ProductBase
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>
        [Required(ErrorMessage = "Product ID is required.")]
        public Guid Id { get; set; }


        /// <summary>
        /// Gets or sets the category details of the product.
        /// This can be null if the category is not provided.
        /// </summary>
        public GetCategory? Category { get; set; }
    }
}
