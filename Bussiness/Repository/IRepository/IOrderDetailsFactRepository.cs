using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IOrderDetailsFactRepository
    {
        Task<IEnumerable<OrderDetailsFactDTO>> GetAllOrderDetailsFactsAsync();
        Task<OrderDetailsFactDTO> GetOrderDetailsFactByIdAsync(int id);
    }
}
