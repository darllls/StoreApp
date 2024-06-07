using DTOs;

namespace StoreWeb.Service.IService
{
    public interface ISupplyService
    {
        Task<List<SupplyDTO>> GetAllSupplies();
        Task<SupplyDTO> GetSupplyById(int supplyId);
        Task<SupplyDTO> CreateSupply(SupplyDTO supplyDto);
        Task<SupplyDTO> UpdateSupply(int supplyId, SupplyDTO supplyDto);
        Task<bool> DeleteSupply(int supplyId);

        Task<List<SupplyStatusDTO>> GetSupplyStatuses();
        Task<List<SupplierDTO>> GetSuppliers();
        Task<SupplierDTO> GetSupplierById(int supplierId);
        Task<List<SupplyDetailsDTO>> GetSupplyDetails(int supplyId);
        Task<List<ShipmentInvoiceDTO>> GetShipmentInvoices(int supplyDetailsId);
        Task<bool> IsSupplyNumberUnique(string supplyNumber);
        Task<IEnumerable<StoreDTO>> GetStoresForEmployee(int employeeId);
    }
}
