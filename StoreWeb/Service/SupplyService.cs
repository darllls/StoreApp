using DTOs;
using Newtonsoft.Json;
using StoreWeb.Service.IService;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace StoreWeb.Service
{
    public class SupplyService : ISupplyService
    {
        private readonly HttpClient _httpClient;

        public SupplyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SupplyDTO> CreateSupply(SupplyDTO supplyDto)
        {
            var supplyJson = JsonConvert.SerializeObject(supplyDto);
            var content = new StringContent(supplyJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/supplies", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed to create supply. Error: {errorMessage}");
                return null; // Or throw an exception based on your error handling strategy
            }

            var createdSupplyJson = await response.Content.ReadAsStringAsync();
            var createdSupply = JsonConvert.DeserializeObject<SupplyDTO>(createdSupplyJson);

            return createdSupply;
        }

        public async Task<bool> DeleteSupply(int supplyId)
        {
            var response = await _httpClient.DeleteAsync($"/api/supplies/{supplyId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<SupplyDTO>> GetAllSupplies()
        {
            var response = await _httpClient.GetAsync("/api/supplies");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var supplies = JsonConvert.DeserializeObject<List<SupplyDTO>>(content);
                return supplies;
            }

            return new List<SupplyDTO>();
        }

        public async Task<SupplyDTO> GetSupplyById(int supplyId)
        {
            var response = await _httpClient.GetAsync($"/api/supplies/{supplyId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var supply = JsonConvert.DeserializeObject<SupplyDTO>(content);
                return supply;
            }

            return new SupplyDTO();
        }

        public async Task<SupplyDTO> UpdateSupply(int supplyId, SupplyDTO supplyDto)
        {
            var supplyJson = JsonConvert.SerializeObject(supplyDto);
            var content = new StringContent(supplyJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/supplies/{supplyId}", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed to update supply. Status: {response.StatusCode}, Error: {errorMessage}");
                throw new HttpRequestException($"Failed to update supply. Status: {response.StatusCode}, Error: {errorMessage}");
            }

            var updatedSupplyJson = await response.Content.ReadAsStringAsync();
            var updatedSupply = JsonConvert.DeserializeObject<SupplyDTO>(updatedSupplyJson);

            return updatedSupply;
        }

        public async Task<List<SupplierDTO>> GetSuppliers()
        {
            var response = await _httpClient.GetAsync("/api/supplies/suppliers");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var suppliers = JsonConvert.DeserializeObject<List<SupplierDTO>>(content);
                return suppliers;
            }

            return new List<SupplierDTO>();
        }

        public async Task<List<SupplyStatusDTO>> GetSupplyStatuses()
        {
            var response = await _httpClient.GetAsync("/api/supplies/statuses");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var supplystatuses = JsonConvert.DeserializeObject<List<SupplyStatusDTO>>(content);
                return supplystatuses;
            }

            return new List<SupplyStatusDTO>();
        }

        public async Task<SupplierDTO> GetSupplierById(int supplierId)
        {
            var response = await _httpClient.GetAsync($"/api/supplies/suppliers/{supplierId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var supplier = JsonConvert.DeserializeObject<SupplierDTO>(content);
                return supplier;
            }

            return new SupplierDTO();
        }

        public async Task<List<SupplyDetailsDTO>> GetSupplyDetails(int supplyId)
        {
            var response = await _httpClient.GetAsync($"/api/supplies/{supplyId}/details");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var supplyDetails = JsonConvert.DeserializeObject<List<SupplyDetailsDTO>>(content);
                return supplyDetails;
            }

            return new List<SupplyDetailsDTO>();
        }

        public async Task<List<ShipmentInvoiceDTO>> GetShipmentInvoices(int supplyDetailsId)
        {
            var response = await _httpClient.GetAsync($"/api/supplies/{supplyDetailsId}/invoices");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var shipmentInvoices = JsonConvert.DeserializeObject<List<ShipmentInvoiceDTO>>(content);
                return shipmentInvoices;
            }

            return new List<ShipmentInvoiceDTO>();
        }

        public async Task<bool> IsSupplyNumberUnique(string supplyNumber)
        {
            var response = await _httpClient.GetAsync($"/api/supplies/unique?number={supplyNumber}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<StoreDTO>> GetStoresForEmployee(int employeeId)
        {
            var response = await _httpClient.GetAsync($"/api/supplies/{employeeId}/stores");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var stores = JsonConvert.DeserializeObject<IEnumerable<StoreDTO>>(content);
                return stores;
            }

            return new List<StoreDTO>();
        }
    }
}
