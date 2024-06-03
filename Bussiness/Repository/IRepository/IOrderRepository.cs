using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bussiness.Repository.IRepository
{
    public interface IOrderRepository
    {
        Task<OrderDTO> CreateOrderAsync(OrderDTO orderDto);
        Task<OrderDTO> UpdateOrderAsync(int orderId, OrderDTO orderDto);
        Task<bool> DeleteOrderAsync(int orderId);
        Task<OrderDTO> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();

        Task<IEnumerable<OrderItemDTO>> GetOrderItemsAsync(int orderId);
    }
}
