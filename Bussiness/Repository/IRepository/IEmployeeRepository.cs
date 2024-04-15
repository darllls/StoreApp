using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeDTO>> GetAllEmployeesAsync();
        Task<EmployeeDTO> GetEmployeeByIdAsync(int employeeId);
        Task<EmployeeDTO> CreateEmployeeAsync(EmployeeDTO employeeDto);
        Task<EmployeeDTO> UpdateEmployeeAsync(int employeeId, EmployeeDTO employeeDto);
        Task<bool> DeleteEmployeeAsync(int employeeId);
    }

}
