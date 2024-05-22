using DTOs;

namespace StoreWeb.Service.IService
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllProducts();
        Task<ProductDTO> GetProductById(int productId);
        Task<ProductDTO> CreateProduct(ProductDTO product);
        Task<ProductDTO> UpdateProduct(int productId, ProductDTO product);
        Task<bool> DeleteProduct(int productId);

        Task<List<BrandDTO>> GetAllBrands();
        Task<List<CategoryDTO>> GetAllCategories();
    }

}
