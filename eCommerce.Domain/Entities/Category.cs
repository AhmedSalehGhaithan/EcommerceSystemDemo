using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Domain.Entities
{
    /// <summary>
    /// Represents a product category in the eCommerce system.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the unique identifier of the category.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// Required and must be between 1 and 100 characters.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the collection of products associated with this category.
        /// This can be null if there are no products in the category.
        /// </summary>
        public ICollection<Product>? Products { get; set; }
    }
}
