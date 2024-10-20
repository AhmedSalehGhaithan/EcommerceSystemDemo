using AutoMapper;
using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.Response;
using eCommerce.Application.Services.Interfaces;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;

namespace eCommerce.Application.Services.Implementations
{
    /// <summary>
    /// Service class for handling category-related operations.
    /// </summary>
    /// <param name="_category">Generic repository for category operations.</param>
    /// <param name="_mapper">Mapper for converting between DTOs and domain entities.</param>
    public class CategoryService(IGeneric<Category> _category, IMapper _mapper) : ICategoryService
    {
        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="entity">Data transfer object for the new category.</param>
        /// <returns>A response indicating the success or failure of the creation.</returns>
        public async Task<ServiceResponse> CreateAsync(CreateCategory entity)
        {
            var mapData = _mapper.Map<Category>(entity);
            var result = await _category.CreateAsync(mapData);
            return CreateResponse(result, "Category Created successfully.", "Category failed to be Created.");
        }

        /// <summary>
        /// Deletes a category by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the category to delete.</param>
        /// <returns>A response indicating the success or failure of the deletion.</returns>
        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            var result = await _category.DeleteAsync(id);
            return CreateResponse(result, "Category Deleted successfully.", "Category failed to be deleted.");
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>A collection of category data transfer objects.</returns>
        public async Task<IEnumerable<GetCategory>> GetAllAsync()
        {
            var rowData = await _category.GetAllAsync();
            return _mapper.Map<IEnumerable<GetCategory>>(rowData);
        }

        /// <summary>
        /// Retrieves a category by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the category.</param>
        /// <returns>The corresponding category data transfer object.</returns>
        public async Task<GetCategory> GetByIdAsync(Guid id)
        {
            var data = await _category.GetByIdAsync(id);
            return _mapper.Map<GetCategory>(data);
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="entity">Data transfer object containing updated category data.</param>
        /// <returns>A response indicating the success or failure of the update.</returns>
        public async Task<ServiceResponse> UpdateAsync(UpdateCategory entity)
        {
            var mapData = _mapper.Map<Category>(entity);
            var result = await _category.UpdateAsync(mapData);
            return CreateResponse(result, "Category Updated successfully.", "Category failed to be Updated.");
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
