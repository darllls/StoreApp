using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface ICustomerRepository
    {
        Task<List<CustomerDTO>> GetAllCustomersAsync();
        Task<CustomerDTO> GetCustomerByIdAsync(int customerId);
        Task<CustomerDTO> CreateCustomerAsync(CustomerDTO customerDto);
        Task<CustomerDTO> UpdateCustomerAsync(int customerId, CustomerDTO customerDto);
        Task<bool> DeleteCustomerAsync(int customerId);

    }
}
