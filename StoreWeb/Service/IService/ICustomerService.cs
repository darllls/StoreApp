using DTOs;

namespace StoreWeb.Service.IService
{
    public interface ICustomerService
    {
        Task<List<CustomerDTO>> GetAllCustomers();
        Task<CustomerDTO> GetCustomerById(int customerId);
        Task<CustomerDTO> CreateCustomer(CustomerDTO customerDto);
        Task<CustomerDTO> UpdateCustomer(int customerId, CustomerDTO customerDto);
        Task<bool> DeleteCustomer(int customerId);
        Task<List<CustomerTypeDTO>> GetAllCustomerTypes();
    }
}
