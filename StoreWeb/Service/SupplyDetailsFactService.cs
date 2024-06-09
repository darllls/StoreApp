using DTOs;
using Newtonsoft.Json;
using StoreWeb.Service.IService;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoreWeb.Service
{
    public class SupplyDetailsFactService : ISupplyDetailsFactService
    {
        private readonly HttpClient _httpClient;

        public SupplyDetailsFactService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SupplyDetailsFactDTO>> GetAllSupplyDetailsFacts()
        {
            var response = await _httpClient.GetAsync("/api/supplydetailsfacts");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var supplyDetailsFacts = JsonConvert.DeserializeObject<List<SupplyDetailsFactDTO>>(content);
                return supplyDetailsFacts;
            }

            return new List<SupplyDetailsFactDTO>();
        }

        public async Task<SupplyDetailsFactDTO> GetSupplyDetailsFactById(int supplyDetailsFactId)
        {
            var response = await _httpClient.GetAsync($"/api/supplydetailsfacts/{supplyDetailsFactId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var supplyDetailsFact = JsonConvert.DeserializeObject<SupplyDetailsFactDTO>(content);
                return supplyDetailsFact;
            }

            return null;
        }
    }
}
