using eCommerce.Application.DTOs.Product;

namespace eCommerce.Application.DTOs.Category
{
    /// <summary>
    /// Data transfer object for retrieving category details.
    /// Inherits from CategoryBase.
    /// </summary>
    public class GetCategory : CategoryBase
    {
        /// <summary>
        /// Gets or sets the unique identifier of the category.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the collection of products associated with this category.
        /// This can be null if there are no products in the category.
        /// </summary>
        public ICollection<GetProduct>? Products { get; set; }
    }
}
