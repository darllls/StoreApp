using DTOs;
using Newtonsoft.Json;
using StoreWeb.Service.IService;
using System.Text;

namespace StoreWeb.Service
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OrderDTO> CreateOrder(OrderDTO orderDto)
        {
            var orderJson = JsonConvert.SerializeObject(orderDto);
            var content = new StringContent(orderJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/orders", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                // Log or handle the error message appropriately
                Console.WriteLine($"Failed to create order. Error: {errorMessage}");
                return null; // Or throw an exception based on your error handling strategy
            }

            var createdOrderJson = await response.Content.ReadAsStringAsync();
            var createdOrder = JsonConvert.DeserializeObject<OrderDTO>(createdOrderJson);

            return createdOrder;
        }

        public async Task<bool> DeleteOrder(int orderId)
        {
            var response = await _httpClient.DeleteAsync($"/api/orders/{orderId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<OrderDTO>> GetAllOrders()
        {
            var response = await _httpClient.GetAsync("/api/orders");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<List<OrderDTO>>(content);
                return orders;
            }

            return new List<OrderDTO>();
        }

        public async Task<OrderDTO> GetOrderById(int orderId)
        {
            var response = await _httpClient.GetAsync($"/api/orders/{orderId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var order = JsonConvert.DeserializeObject<OrderDTO>(content);
                return order;
            }

            return new OrderDTO();
        }

        public async Task<OrderDTO> UpdateOrder(int orderId, OrderDTO orderDto)
        {
            var orderJson = JsonConvert.SerializeObject(orderDto);
            var content = new StringContent(orderJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/orders/{orderId}", content);
            response.EnsureSuccessStatusCode();

            var updatedOrderJson = await response.Content.ReadAsStringAsync();
            var updatedOrder = JsonConvert.DeserializeObject<OrderDTO>(updatedOrderJson);

            return updatedOrder;
        }

        public async Task<List<OrderItemDTO>> GetOrderItems(int orderId)
        {
            var response = await _httpClient.GetAsync($"/api/orders/{orderId}/items");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var orderItems = JsonConvert.DeserializeObject<List<OrderItemDTO>>(content);
                return orderItems;
            }

            return new List<OrderItemDTO>();
        }

        public async Task<bool> IsOrderNumberUnique(string orderNumber)
        {
            var response = await _httpClient.GetAsync($"/api/orders/unique?number={orderNumber}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<AvailableProductDTO>> GetAvailableProductsForEmployee(int employeeId)
        {
            var response = await _httpClient.GetAsync($"/api/orders/{employeeId}/available-products");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var availableProducts = JsonConvert.DeserializeObject<IEnumerable<AvailableProductDTO>>(content);
                return availableProducts;
            }

            return new List<AvailableProductDTO>();
        }
    }
}
