using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreWeb.Service.IService
{
    public interface ISupplyDetailsFactService
    {
        Task<List<SupplyDetailsFactDTO>> GetAllSupplyDetailsFacts();
        Task<SupplyDetailsFactDTO> GetSupplyDetailsFactById(int supplyDetailsFactId);
    }
}
