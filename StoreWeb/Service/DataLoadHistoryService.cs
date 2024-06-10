using DTOs;
using Newtonsoft.Json;
using System.Collections.Generic;
using StoreWeb.Service.IService;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Service
{
    public class DataLoadHistoryService : IDataLoadHistoryService
    {
        private readonly HttpClient _httpClient;

        public DataLoadHistoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DataLoadHistoryDTO>> GetAllDataLoadHistoriesAsync()
        {
            var response = await _httpClient.GetAsync("/api/dataloadhistory");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<DataLoadHistoryDTO>>(content);
        }

        public async Task<DataLoadHistoryDTO> GetLatestDataLoadHistoryAsync()
        {
            var response = await _httpClient.GetAsync("/api/dataloadhistory/latest");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DataLoadHistoryDTO>(content);
        }
    }
}