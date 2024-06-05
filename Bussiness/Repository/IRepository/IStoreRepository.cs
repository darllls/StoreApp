using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IStoreRepository
    {
        Task<List<StoreDTO>> GetAllStoresAsync();
        Task<StoreDTO> GetStoreByIdAsync(int storeId);

    }
}
