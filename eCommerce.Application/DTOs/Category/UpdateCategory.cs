using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Category
{
    /// <summary>
    /// Data transfer object for updating a category.
    /// Inherits from CategoryBase to include common category properties.
    /// </summary>
    public class UpdateCategory : CategoryBase
    {
        /// <summary>
        /// Gets or sets the unique identifier of the category to be updated.
        /// Required.
        /// </summary>
        [Required(ErrorMessage = "Category ID is required.")]
        public Guid Id { get; set; }
    }
}
