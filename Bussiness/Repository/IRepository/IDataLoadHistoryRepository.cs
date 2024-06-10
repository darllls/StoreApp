using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IDataLoadHistoryRepository
    {
        Task<List<DataLoadHistoryDTO>> GetAllDataLoadHistoriesAsync();
        Task<DataLoadHistoryDTO> GetLatestDataLoadHistoryAsync();
    }
}
