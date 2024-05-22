using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bussiness.Repository.IRepository
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int categoryId);
    }
}
