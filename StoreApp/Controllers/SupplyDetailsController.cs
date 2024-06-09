using Business.Repository.IRepository;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreApp.Controllers
{
    [Route("api/supplydetailsfacts")]
    [ApiController]
    public class SupplyDetailsFactController : ControllerBase
    {
        private readonly ISupplyDetailsFactRepository _supplyDetailsFactRepository;

        public SupplyDetailsFactController(ISupplyDetailsFactRepository supplyDetailsFactRepository)
        {
            _supplyDetailsFactRepository = supplyDetailsFactRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<SupplyDetailsFactDTO>>> GetAllSupplyDetailsFacts()
        {
            var supplyDetailsFacts = await _supplyDetailsFactRepository.GetAllSupplyDetailsFactsAsync();
            return Ok(supplyDetailsFacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplyDetailsFactDTO>> GetSupplyDetailsFactById(int id)
        {
            var supplyDetailsFact = await _supplyDetailsFactRepository.GetSupplyDetailsFactByIdAsync(id);
            if (supplyDetailsFact == null)
                return NotFound();

            return Ok(supplyDetailsFact);
        }
    }
}
