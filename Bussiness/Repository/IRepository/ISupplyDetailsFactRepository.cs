using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface ISupplyDetailsFactRepository
    {
        Task<IEnumerable<SupplyDetailsFactDTO>> GetAllSupplyDetailsFactsAsync();
        Task<SupplyDetailsFactDTO> GetSupplyDetailsFactByIdAsync(int id);
    }
}
