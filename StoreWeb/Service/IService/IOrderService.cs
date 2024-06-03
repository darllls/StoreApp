using DTOs;

namespace StoreWeb.Service.IService
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAllOrders();
        Task<OrderDTO> GetOrderById(int orderId);
        Task<OrderDTO> CreateOrder(OrderDTO order);
        Task<OrderDTO> UpdateOrder(int orderId, OrderDTO order);
        Task<bool> DeleteOrder(int orderId);
        Task<List<OrderItemDTO>> GetOrderItems(int orderId);

    }

}
