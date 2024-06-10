using System.Threading.Tasks;

namespace StoreWeb.Service.IService
{
    public interface IProcedureService
    {
        Task ClearWarehouseAsync();
        Task PrimaryFillStagingAsync();
        Task PrimaryFillWarehouseAsync();
        Task IncrementalFillStagingAsync();
        Task IncrementalFillWarehouseAsync();
    }
}
