using Microsoft.AspNetCore.Mvc;
using Business.Repository.IRepository;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/procedures")]
    public class ProcedureController : ControllerBase
    {
        private readonly IProcedureRepository _procedureRepository;

        public ProcedureController(IProcedureRepository procedureRepository)
        {
            _procedureRepository = procedureRepository;
        }

        [HttpPost("clear-warehouse")]
        public async Task<IActionResult> ClearWarehouse()
        {
            await _procedureRepository.ClearWarehouseAsync();
            return NoContent();
        }

        [HttpPost("primary-fill-staging")]
        public async Task<IActionResult> PrimaryFillStaging()
        {
            await _procedureRepository.PrimaryFillStagingAsync();
            return NoContent();
        }

        [HttpPost("primary-fill-warehouse")]
        public async Task<IActionResult> PrimaryFillWarehouse()
        {
            await _procedureRepository.PrimaryFillWarehouseAsync();
            return NoContent();
        }

        [HttpPost("incremental-fill-staging")]
        public async Task<IActionResult> IncrementalFillStaging()
        {
            await _procedureRepository.IncrementalFillStagingAsync();
            return NoContent();
        }

        [HttpPost("incremental-fill-warehouse")]
        public async Task<IActionResult> IncrementalFillWarehouse()
        {
            await _procedureRepository.IncrementalFillWarehouseAsync();
            return NoContent();
        }
    }
}
