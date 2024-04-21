using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using Newtonsoft.Json;
using StoreWeb.Service.IService;

namespace StoreWeb.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EmployeeDTO> CreateEmployee(EmployeeDTO employeeDto)
        {
            var employeeJson = JsonConvert.SerializeObject(employeeDto);
            var content = new StringContent(employeeJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/employees", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                // Log or handle the error message appropriately
                Console.WriteLine($"Failed to create employee. Error: {errorMessage}");
                return null; // Or throw an exception based on your error handling strategy
            }

            var createdEmployeeJson = await response.Content.ReadAsStringAsync();
            var createdEmployee = JsonConvert.DeserializeObject<EmployeeDTO>(createdEmployeeJson);

            return createdEmployee;
        }

        public async Task<bool> DeleteEmployee(int employeeId)
        {
            var response = await _httpClient.DeleteAsync($"/api/employees/{employeeId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<EmployeeDTO>> GetAllEmployees()
        {
            var response = await _httpClient.GetAsync("/api/employees");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var employees = JsonConvert.DeserializeObject<List<EmployeeDTO>>(content);
                return employees;
            }

            return new List<EmployeeDTO>();
        }

        public async Task<EmployeeDTO> GetEmployeeById(int employeeId)
        {
            var response = await _httpClient.GetAsync($"/api/employees/{employeeId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var employee = JsonConvert.DeserializeObject<EmployeeDTO>(content);
                return employee;
            }

            return new EmployeeDTO();
        }

        public async Task<EmployeeDTO> UpdateEmployee(int employeeId, EmployeeDTO employeeDto)
        {
            var employeeJson = JsonConvert.SerializeObject(employeeDto);
            var content = new StringContent(employeeJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/employees/{employeeId}", content);
            response.EnsureSuccessStatusCode();

            var updatedEmployeeJson = await response.Content.ReadAsStringAsync();
            var updatedEmployee = JsonConvert.DeserializeObject<EmployeeDTO>(updatedEmployeeJson);

            return updatedEmployee;
        }

        public async Task<List<StoreDTO>> GetAllStores()
        {
            var response = await _httpClient.GetAsync("/api/stores");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var stores = JsonConvert.DeserializeObject<List<StoreDTO>>(content);
                return stores;
            }

            return new List<StoreDTO>();
        }
    }

    
}
