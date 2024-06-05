using Business.Repository.IRepository;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _repository.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProduct(int productId)
        {
            var product = await _repository.GetProductByIdAsync(productId);
            if (product == null)
                return NotFound();

            return Ok(product);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDTO productDto)
        {
            var createdProduct = await _repository.CreateProductAsync(productDto);
            return Ok(createdProduct);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, ProductDTO productDto)
        {
            var updatedProduct = await _repository.UpdateProductAsync(productId, productDto);
            if (updatedProduct == null)
                return NotFound();

            return Ok(updatedProduct);
        }

        
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var result = await _repository.DeleteProductAsync(productId);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
