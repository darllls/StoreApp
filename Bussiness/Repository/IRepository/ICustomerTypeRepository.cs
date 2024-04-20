using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Repository.IRepository
{
    public interface ICustomerTypeRepository
    {
        Task<List<CustomerTypeDTO>> GetAllCustomerTypesAsync();
        Task<CustomerTypeDTO> GetCustomerTypeByIdAsync(int customertypeId);
        Task<CustomerTypeDTO> CreateCustomerTypeAsync(CustomerTypeDTO customertypeDto);
        Task<CustomerTypeDTO> UpdateCustomerTypeAsync(int customertypeId, CustomerTypeDTO customertypeDto);
        Task<bool> DeleteCustomerTypeAsync(int customertypeId);
    }
}
