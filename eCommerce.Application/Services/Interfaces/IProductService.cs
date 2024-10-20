using eCommerce.Application.DTOs.Product;
using eCommerce.Application.DTOs.Response;

namespace eCommerce.Application.Services.Interfaces
{
    /// <summary>
    /// Interface for product-related service operations.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A collection of product data transfer objects.</returns>
        Task<IEnumerable<GetProduct>> GetAllAsync();

        /// <summary>
        /// Retrieves a product by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>The corresponding product data transfer object.</returns>
        Task<GetProduct> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="entity">Data transfer object for the new product.</param>
        /// <returns>A response indicating the success or failure of the creation.</returns>
        Task<ServiceResponse> CreateAsync(CreateProduct entity);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="entity">Data transfer object containing updated product data.</param>
        /// <returns>A response indicating the success or failure of the update.</returns>
        Task<ServiceResponse> UpdateAsync(UpdateProduct entity);

        /// <summary>
        /// Deletes a product by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete.</param>
        /// <returns>A response indicating the success or failure of the deletion.</returns>
        Task<ServiceResponse> DeleteAsync(Guid id);
    }
}
