using Business.Repository.IRepository;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Controllers
{
    [Route("api/supplies")]
    [ApiController]
    public class SupplyController : ControllerBase
    {
        private readonly ISupplyRepository _supplyRepository;

        public SupplyController(ISupplyRepository supplyRepository)
        {
            _supplyRepository = supplyRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<SupplyDTO>>> GetAllSupplies()
        {
            var supplies = await _supplyRepository.GetAllSuppliesAsync();
            return Ok(supplies);
        }

        [HttpGet("suppliers")]
        public async Task<ActionResult<List<SupplierDTO>>> GetSuppliers()
        {
            var suppliers = await _supplyRepository.GetSuppliersAsync();
            return Ok(suppliers);
        }

        [HttpGet("statuses")]
        public async Task<ActionResult<List<SupplyStatusDTO>>> GetSupplyStatuses()
        {
            var supplystatuses = await _supplyRepository.GetAllSupplyStatusesAsync();
            return Ok(supplystatuses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplyDTO>> GetSupplyById(int id)
        {
            var supply = await _supplyRepository.GetSupplyByIdAsync(id);
            if (supply == null)
                return NotFound();

            return Ok(supply);
        }

        [HttpGet("suppliers/{id}")]
        public async Task<ActionResult<SupplierDTO>> GetSupplierById(int id)
        {
            var supplier = await _supplyRepository.GetSupplierByIdAsync(id);
            if (supplier == null)
                return NotFound();

            return Ok(supplier);
        }

        [HttpPost]
        public async Task<ActionResult<SupplyDTO>> CreateSupply(SupplyDTO supplyDto)
        {
            var createdSupply = await _supplyRepository.CreateSupplyAsync(supplyDto);
            return CreatedAtAction(nameof(GetSupplyById), new { id = createdSupply.SupplyId }, createdSupply);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SupplyDTO>> UpdateSupply(int id, SupplyDTO supplyDto)
        {
            var updatedSupply = await _supplyRepository.UpdateSupplyAsync(id, supplyDto);
            if (updatedSupply == null)
                return NotFound();

            return Ok(updatedSupply);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSupply(int id)
        {
            var result = await _supplyRepository.DeleteSupplyAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("{supplyId}/details")]
        public async Task<ActionResult<IEnumerable<SupplyDetailsDTO>>> GetSupplyDetails(int supplyId)
        {
            var supplyDetails = await _supplyRepository.GetSupplyDetailsAsync(supplyId);
            if (supplyDetails == null)
                return NotFound();

            return Ok(supplyDetails);
        }

        [HttpGet("{supplyDetailsId}/invoices")]
        public async Task<ActionResult<IEnumerable<ShipmentInvoiceDTO>>> GetShipmentInvoices(int supplyDetailsId)
        {
            var shipmentInvoice = await _supplyRepository.GetShipmentInvoicesAsync(supplyDetailsId);
            if (shipmentInvoice == null)
                return NotFound();

            return Ok(shipmentInvoice);
        }

        [HttpGet("unique")]
        public async Task<ActionResult<bool>> IsSupplyNumberUnique(string number)
        {
            var isUnique = await _supplyRepository.IsSupplyNumberUniqueAsync(number);
            return Ok(isUnique);
        }

        [HttpGet("{employeeId}/stores")]
        public async Task<ActionResult<IEnumerable<StoreDTO>>> GetStoresForEmployeeAndProduct(int employeeId)
        {
            var stores = await _supplyRepository.GetStoresForEmployeeAsync(employeeId);
            return Ok(stores);
        }
    }
}
