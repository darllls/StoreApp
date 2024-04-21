using DTOs;
using Newtonsoft.Json;
using StoreWeb.Service.IService;
using System.Text;

namespace StoreWeb.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CustomerDTO> CreateCustomer(CustomerDTO customerDto)
        {
            var customerJson = JsonConvert.SerializeObject(customerDto);
            var content = new StringContent(customerJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/customers", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                // Log or handle the error message appropriately
                Console.WriteLine($"Failed to create customer. Error: {errorMessage}");
                return null; // Or throw an exception based on your error handling strategy
            }

            var createdCustomerJson = await response.Content.ReadAsStringAsync();
            var createdCustomer = JsonConvert.DeserializeObject<CustomerDTO>(createdCustomerJson);

            return createdCustomer;
        }

        public async Task<bool> DeleteCustomer(int customerId)
        {
            var response = await _httpClient.DeleteAsync($"/api/customers/{customerId}");
            return response.IsSuccessStatusCode;
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

        public async Task<CustomerDTO> UpdateCustomer(int customerId, CustomerDTO customerDto)
        {
            var customerJson = JsonConvert.SerializeObject(customerDto);
            var content = new StringContent(customerJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/customers/{customerId}", content);
            response.EnsureSuccessStatusCode();

            var updatedCustomerJson = await response.Content.ReadAsStringAsync();
            var updatedCustomer = JsonConvert.DeserializeObject<CustomerDTO>(updatedCustomerJson);

            return updatedCustomer;
        }

        public async Task<List<CustomerTypeDTO>> GetAllCustomerTypes()
        {
            var response = await _httpClient.GetAsync("/api/customertypes"); 
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
