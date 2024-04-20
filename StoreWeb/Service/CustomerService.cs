using DTOs;
using Newtonsoft.Json;
using StoreWeb.Service.IService;

namespace StoreWeb.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<CustomerDTO> CreateCustomer(CustomerDTO customerDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CustomerDTO>> GetAllCustomers()
        {
            var response = await _httpClient.GetAsync("/api/customers");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var customers = JsonConvert.DeserializeObject<List<CustomerDTO>>(content);
                return customers;
            }

            return new List<CustomerDTO>();
        }

        public async Task<CustomerDTO> GetCustomerById(int customerId)
        {
            var response = await _httpClient.GetAsync($"/api/customers/{customerId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var customer = JsonConvert.DeserializeObject<CustomerDTO>(content);
                return customer;
            }
            
            return new CustomerDTO();
        }

        public Task<CustomerDTO> UpdateCustomer(int customerId, CustomerDTO customerDto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CustomerTypeDTO>> GetAllCustomerTypes()
        {
            var response = await _httpClient.GetAsync("/api/customertypes"); // Припустимо, що у вас є API endpoint для отримання типів клієнтів
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var customerTypes = JsonConvert.DeserializeObject<List<CustomerTypeDTO>>(content);
                return customerTypes;
            }

            return new List<CustomerTypeDTO>();
        }
    }
}
