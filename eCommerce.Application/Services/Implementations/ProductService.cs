using AutoMapper;
using eCommerce.Application.DTOs.Product;
using eCommerce.Application.DTOs.Response;
using eCommerce.Application.Services.Interfaces;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;

namespace eCommerce.Application.Services.Implementations
{
    /// <summary>
    /// Service class for handling product-related operations.
    /// </summary>
    public class ProductService(IGeneric<Product> _productRepository, IMapper _mapper) : IProductService
    {
        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="createProductDto">Data transfer object for the new product.</param>
        /// <returns>A response indicating the success or failure of the creation.</returns>
        public async Task<ServiceResponse> CreateAsync(CreateProduct createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            int result = await _productRepository.CreateAsync(product);
            return CreateResponse(result, "Product Created successfully.", "Product failed to be Created.");
        }

        /// <summary>
        /// Deletes a product by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete.</param>
        /// <returns>A response indicating the success or failure of the deletion.</returns>
        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await _productRepository.DeleteAsync(id);
            return CreateResponse(result, "Product Deleted successfully.", "Product failed to be deleted.");
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A collection of product data transfer objects.</returns>
        public async Task<IEnumerable<GetProduct>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetProduct>>(products);
        }

        /// <summary>
        /// Retrieves a product by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>The corresponding product data transfer object.</returns>
        public async Task<GetProduct> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<GetProduct>(product);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="updateProductDto">Data transfer object containing updated product data.</param>
        /// <returns>A response indicating the success or failure of the update.</returns>
        public async Task<ServiceResponse> UpdateAsync(UpdateProduct updateProductDto)
        {
            var product = _mapper.Map<Product>(updateProductDto);
            int result = await _productRepository.UpdateAsync(product);
            return CreateResponse(result, "Product Updated successfully.", "Product failed to be Updated.");
        }

        /// <summary>
        /// Creates a service response based on the result of an operation.
        /// </summary>
        /// <param name="result">The result of the operation (number of affected rows).</param>
        /// <param name="successMessage">Message to return on success.</param>
        /// <param name="failureMessage">Message to return on failure.</param>
        /// <returns>A ServiceResponse indicating the outcome.</returns>
        private ServiceResponse CreateResponse(int result, string successMessage, string failureMessage)
        {
            return result > 0 ? new ServiceResponse(true, successMessage)
                              : new ServiceResponse(false, failureMessage);
        }
    }
}
