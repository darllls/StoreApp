using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Repository.IRepository
{
    public interface IProductRepository
    {
        Task<ProductDTO> CreateProductAsync(ProductDTO productDto);
        Task<ProductDTO> UpdateProductAsync(int productId, ProductDTO productDto);
        Task<bool> DeleteProductAsync(int productId);
        Task<ProductDTO> GetProductByIdAsync(int productId);
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();

    }
}
