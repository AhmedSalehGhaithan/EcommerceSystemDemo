using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Host.Controllers
{
    /// <summary>
    /// Controller for managing Product.
    /// </summary>
    /// <param name="_service">Service for product operations.</param>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService _service) : ControllerBase
    {
        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of products or a not found response.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return data.Any() ? Ok(data) : NotFound(data);
        }

        /// <summary>
        /// Retrieves a single product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product data or a not found response.</returns>
        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetAll(Guid id)
        {
            var data = await _service.GetByIdAsync(id);
            return data != null ? Ok(data) : NotFound(data);
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="product">The product data to create.</param>
        /// <returns>The created product or a bad request response.</returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add(CreateProduct product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _service.CreateAsync(product);
            return result.Flag ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="product">The product data to update.</param>
        /// <returns>The updated product or a bad request response.</returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateProduct product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _service.UpdateAsync(product);
            return result.Flag ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>A success or bad request response.</returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return result.Flag ? Ok(result) : BadRequest(result);
        }
    }
}
