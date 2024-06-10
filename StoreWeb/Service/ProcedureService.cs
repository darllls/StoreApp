using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StoreWeb.Service.IService;

namespace StoreWeb.Service
{
    public class ProcedureService : IProcedureService
    {
        private readonly HttpClient _httpClient;

        public ProcedureService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task ClearWarehouseAsync()
        {
            var response = await _httpClient.PostAsync("/api/procedures/clear-warehouse", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task PrimaryFillStagingAsync()
        {
            var response = await _httpClient.PostAsync("/api/procedures/primary-fill-staging", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task PrimaryFillWarehouseAsync()
        {
            var response = await _httpClient.PostAsync("/api/procedures/primary-fill-warehouse", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task IncrementalFillStagingAsync()
        {
            var response = await _httpClient.PostAsync("/api/procedures/incremental-fill-staging", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task IncrementalFillWarehouseAsync()
        {
            var response = await _httpClient.PostAsync("/api/procedures/incremental-fill-warehouse", null);
            response.EnsureSuccessStatusCode();
        }
    }
}
