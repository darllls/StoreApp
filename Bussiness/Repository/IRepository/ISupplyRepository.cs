using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface ISupplyRepository
    {
        Task<SupplyDTO> CreateSupplyAsync(SupplyDTO supplyDto); //створити Supply який має SupplyDetails, які мають ShipmentInvoice
        Task<SupplyDTO> UpdateSupplyAsync(int supplyId, SupplyDTO supplyDto);
        Task<bool> DeleteSupplyAsync(int supplyId); //видалити Supply, його SupplyDetails та ShipmentInvoice
        Task<SupplyDTO> GetSupplyByIdAsync(int supplyId);
        Task<IEnumerable<SupplyDTO>> GetAllSuppliesAsync();

        Task<IEnumerable<SupplierDTO>> GetSuppliersAsync();
        Task<SupplierDTO> GetSupplierByIdAsync(int supplierId);
        Task<IEnumerable<SupplyDetailsDTO>> GetSupplyDetailsAsync(int supplyId);
        Task<IEnumerable<ShipmentInvoiceDTO>> GetShipmentInvoicesAsync(int supplyDetailsId);
        Task<IEnumerable<StoreDTO>> GetStoresForEmployeeAsync(int employeeId); //в який магазин здійснено ShipmentInvoice залежно від працівника який її оформив та товару в SupplyDetails
        Task<bool> IsSupplyNumberUniqueAsync(string supplyNumber);
    }
}
