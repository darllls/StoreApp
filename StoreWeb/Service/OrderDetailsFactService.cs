using DTOs;
using Newtonsoft.Json;
using StoreWeb.Service.IService;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoreWeb.Service
{
    public class OrderDetailsFactService : IOrderDetailsFactService
    {
        private readonly HttpClient _httpClient;

        public OrderDetailsFactService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<OrderDetailsFactDTO>> GetAllOrderDetailsFacts()
        {
            var response = await _httpClient.GetAsync("/api/orderdetailsfacts");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var orderDetailsFacts = JsonConvert.DeserializeObject<List<OrderDetailsFactDTO>>(content);
                return orderDetailsFacts;
            }

            return new List<OrderDetailsFactDTO>();
        }

        public async Task<OrderDetailsFactDTO> GetOrderDetailsFactById(int orderDetailsFactId)
        {
            var response = await _httpClient.GetAsync($"/api/orderdetailsfacts/{orderDetailsFactId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var orderDetailsFact = JsonConvert.DeserializeObject<OrderDetailsFactDTO>(content);
                return orderDetailsFact;
            }

            return null;
        }
    }
}
