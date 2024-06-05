using Business.Repository.IRepository;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeDTO>>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            return Ok(employees);
        }


        [HttpGet("{employeeId}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> CreateEmployee(EmployeeDTO employeeDto)
        {
            var createdEmployee = await _employeeRepository.CreateEmployeeAsync(employeeDto);
            return CreatedAtAction(nameof(GetEmployee), new { employeeId = createdEmployee.EmployeeId }, createdEmployee);
        }

        [HttpPut("{employeeId}")]
        public async Task<ActionResult<EmployeeDTO>> UpdateEmployee(int employeeId, EmployeeDTO employeeDto)
        {
            var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(employeeId, employeeDto);
            if (updatedEmployee == null)
                return NotFound();

            return Ok(updatedEmployee);
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            var result = await _employeeRepository.DeleteEmployeeAsync(employeeId);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
