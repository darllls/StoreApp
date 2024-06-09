using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreWeb.Service.IService
{
    public interface IOrderDetailsFactService
    {
        Task<List<OrderDetailsFactDTO>> GetAllOrderDetailsFacts();
        Task<OrderDetailsFactDTO> GetOrderDetailsFactById(int orderDetailsFactId);
    }
}
