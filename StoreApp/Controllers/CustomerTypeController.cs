using Business.Repository.IRepository;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/customertypes")]
    [ApiController]
    public class CustomerTypeController : ControllerBase
    {
        private readonly ICustomerTypeRepository _customertypeRepository;

        public CustomerTypeController(ICustomerTypeRepository customertypeRepository)
        {
            _customertypeRepository = customertypeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerTypeDTO>>> GetAllCustomerTypes()
        {
            var customertypes = await _customertypeRepository.GetAllCustomerTypesAsync();
            return Ok(customertypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerTypeDTO>> GetCustomerTypeById(int id)
        {
            var customertype = await _customertypeRepository.GetCustomerTypeByIdAsync(id);
            if (customertype == null)
                return NotFound();

            return Ok(customertype);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerTypeDTO>> CreateCustomerType(CustomerTypeDTO customertypeDto)
        {
            var createdCustomerType = await _customertypeRepository.CreateCustomerTypeAsync(customertypeDto);
            return CreatedAtAction(nameof(GetCustomerTypeById), new { id = createdCustomerType.CustomerTypeId }, createdCustomerType);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerTypeDTO>> UpdateCustomerType(int id, CustomerTypeDTO customertypeDto)
        {
            var updatedCustomerType = await _customertypeRepository.UpdateCustomerTypeAsync(id, customertypeDto);
            if (updatedCustomerType == null)
                return NotFound();

            return Ok(updatedCustomerType);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomerType(int id)
        {
            var result = await _customertypeRepository.DeleteCustomerTypeAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }

}
