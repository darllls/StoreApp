using DTOs;
using Newtonsoft.Json;
using StoreWeb.Service.IService;
using System.Text;

namespace StoreWeb.Service
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductDTO> CreateProduct(ProductDTO productDto)
        {
            var productJson = JsonConvert.SerializeObject(productDto);
            var content = new StringContent(productJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/products", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                // Log or handle the error message appropriately
                Console.WriteLine($"Failed to create product. Error: {errorMessage}");
                return null; // Or throw an exception based on your error handling strategy
            }

            var createdProductJson = await response.Content.ReadAsStringAsync();
            var createdProduct = JsonConvert.DeserializeObject<ProductDTO>(createdProductJson);

            return createdProduct;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var response = await _httpClient.DeleteAsync($"/api/products/{productId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<ProductDTO>> GetAllProducts()
        {
            var response = await _httpClient.GetAsync("/api/products");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<ProductDTO>>(content);
                return products;
            }

            return new List<ProductDTO>();
        }

        public async Task<ProductDTO> GetProductById(int productId)
        {
            var response = await _httpClient.GetAsync($"/api/products/{productId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<ProductDTO>(content);
                return product;
            }

            return new ProductDTO();
        }

        public async Task<ProductDTO> UpdateProduct(int productId, ProductDTO productDto)
        {
            var productJson = JsonConvert.SerializeObject(productDto);
            var content = new StringContent(productJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/products/{productId}", content);
            response.EnsureSuccessStatusCode();

            var updatedProductJson = await response.Content.ReadAsStringAsync();
            var updatedProduct = JsonConvert.DeserializeObject<ProductDTO>(updatedProductJson);

            return updatedProduct;
        }
    }
}
