using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IBrandRepository
    {
        Task<List<BrandDTO>> GetAllBrandsAsync();
        Task<BrandDTO> GetBrandByIdAsync(int brandId);

    }
}
