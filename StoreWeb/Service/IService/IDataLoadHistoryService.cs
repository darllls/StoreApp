using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreWeb.Service.IService
{
    public interface IDataLoadHistoryService
    {
        Task<List<DataLoadHistoryDTO>> GetAllDataLoadHistoriesAsync();
        Task<DataLoadHistoryDTO> GetLatestDataLoadHistoryAsync();
    }
}