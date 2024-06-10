using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IProcedureRepository
    {
        Task ClearWarehouseAsync();
        Task PrimaryFillStagingAsync();
        Task PrimaryFillWarehouseAsync();
        Task IncrementalFillStagingAsync();
        Task IncrementalFillWarehouseAsync();
    }
}
