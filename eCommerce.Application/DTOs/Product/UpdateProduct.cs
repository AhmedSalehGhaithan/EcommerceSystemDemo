using System;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Product
{
    /// <summary>
    /// Data transfer object for updating a product.
    /// Inherits from ProductBase.
    /// </summary>
    public class UpdateProduct : ProductBase
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product to be updated.
        /// Required.
        /// </summary>
        [Required(ErrorMessage = "Product ID is required.")]
        public Guid Id { get; set; }
    }
}
