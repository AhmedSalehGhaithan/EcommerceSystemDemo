using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Category
{
    /// <summary>
    /// Base class for category data transfer objects.
    /// </summary>
    public class CategoryBase
    {
        /// <summary>
        /// Gets or sets the name of the category.
        /// Required and must be between 1 and 100 characters.
        /// </summary>
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Category name must be between 1 and 100 characters.")]
        public string? Name { get; set; }
    }
}
