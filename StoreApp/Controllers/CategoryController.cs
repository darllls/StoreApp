using Business.Repository.IRepository;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return Ok(categories);
        }


        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (category == null)
                return NotFound();

            return Ok(category);
        }
    }
}
