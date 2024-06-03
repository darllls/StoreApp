using Bussiness.Repository.IRepository;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDTO>>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrderById(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> CreateOrder(OrderDTO orderDto)
        {
            var createdOrder = await _orderRepository.CreateOrderAsync(orderDto);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderId }, createdOrder);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDTO>> UpdateOrder(int id, OrderDTO orderDto)
        {
            var updatedOrder = await _orderRepository.UpdateOrderAsync(id, orderDto);
            if (updatedOrder == null)
                return NotFound();

            return Ok(updatedOrder);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var result = await _orderRepository.DeleteOrderAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("{orderId}/items")]
        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetOrderItems(int orderId)
        {
            var orderItems = await _orderRepository.GetOrderItemsAsync(orderId);
            if (orderItems == null)
                return NotFound();

            return Ok(orderItems);
        }
    }

}
