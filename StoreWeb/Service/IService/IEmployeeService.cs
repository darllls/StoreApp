using DTOs;

namespace StoreWeb.Service.IService
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetAllEmployees();
        Task<EmployeeDTO> GetEmployeeById(int employeeId);
        Task<EmployeeDTO> CreateEmployee(EmployeeDTO employeeDto);
        Task<EmployeeDTO> UpdateEmployee(int employeeId, EmployeeDTO employeeDto);
        Task<bool> DeleteEmployee(int employeeId);

        Task<List<StoreDTO>> GetAllStores();
    }
}
