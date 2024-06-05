using Business.Repository.IRepository;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Controllers
{
    [ApiController]
    [Route("api/stores")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;

        public StoreController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<StoreDTO>>> GetAllStores()
        {
            var stores = await _storeRepository.GetAllStoresAsync();
            return Ok(stores);
        }


        [HttpGet("{storeId}")]
        public async Task<ActionResult<StoreDTO>> GetStore(int storeId)
        {
            var store = await _storeRepository.GetStoreByIdAsync(storeId);
            if (store == null)
                return NotFound();

            return Ok(store);
        }
    }
}
