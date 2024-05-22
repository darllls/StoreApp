using Bussiness.Repository.IRepository;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Controllers
{
    [ApiController]
    [Route("api/brands")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;

        public BrandController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<BrandDTO>>> GetAllBrands()
        {
            var brands = await _brandRepository.GetAllBrandsAsync();
            return Ok(brands);
        }


        [HttpGet("{brandId}")]
        public async Task<ActionResult<BrandDTO>> GetBrand(int brandId)
        {
            var brand = await _brandRepository.GetBrandByIdAsync(brandId);
            if (brand == null)
                return NotFound();

            return Ok(brand);
        }
    }
}
