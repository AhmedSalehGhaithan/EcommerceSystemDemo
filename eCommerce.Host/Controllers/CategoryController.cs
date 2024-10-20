using eCommerce.Application.DTOs.Category;
using eCommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Host.Controllers
{
    /// <summary>
    /// Controller for managing categories.
    /// </summary>
    /// <param name="_service">Service for category operations.</param>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService _service) : ControllerBase
    {
        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>A list of categories or a not found response.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return data.Any() ? Ok(data) : NotFound(data);
        }

        /// <summary>
        /// Retrieves a single category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        /// <returns>The category data or a not found response.</returns>
        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetAll(Guid id)
        {
            var data = await _service.GetByIdAsync(id);
            return data != null ? Ok(data) : NotFound(data);
        }

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="product">The category data to create.</param>
        /// <returns>The created category or a bad request response.</returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add(CreateCategory product)
        {
            var result = await _service.CreateAsync(product);
            return result.Flag ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="product">The category data to update.</param>
        /// <returns>The updated category or a bad request response.</returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateCategory product)
        {
            var result = await _service.UpdateAsync(product);
            return result.Flag ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>A success or bad request response.</returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return result.Flag ? Ok(result) : BadRequest(result);
        }
    }
}
