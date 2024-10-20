using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.Product;
using eCommerce.Application.DTOs.Response;

namespace eCommerce.Application.Services.Interfaces
{
    /// <summary>
    /// Interface for category-related service operations.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>A collection of category data transfer objects.</returns>
        Task<IEnumerable<GetCategory>> GetAllAsync();

        /// <summary>
        /// Retrieves a category by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the category.</param>
        /// <returns>The corresponding category data transfer object.</returns>
        Task<GetCategory> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="entity">Data transfer object for the new category.</param>
        /// <returns>A response indicating the success or failure of the creation.</returns>
        Task<ServiceResponse> CreateAsync(CreateCategory entity);

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="entity">Data transfer object containing updated category data.</param>
        /// <returns>A response indicating the success or failure of the update.</returns>
        Task<ServiceResponse> UpdateAsync(UpdateCategory entity);

        /// <summary>
        /// Deletes a category by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the category to delete.</param>
        /// <returns>A response indicating the success or failure of the deletion.</returns>
        Task<ServiceResponse> DeleteAsync(Guid id);
    }
}
