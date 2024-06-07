using AutoMapper;
using Business.Repository.IRepository;
using DataContext.Data;
using DataContext.Models;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class SupplyRepository : ISupplyRepository
    {
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;

        public SupplyRepository(StoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<SupplyDTO> CreateSupplyAsync(SupplyDTO supplyDto)
        {
            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(s => s.SupplierId == supplyDto.SupplierId);

            if (supplier == null)
            {
                throw new Exception("Supplier not found");
            }

            var supply = _mapper.Map<Supply>(supplyDto);
            supply.Status = await _context.SupplyStatuses.FindAsync(supplyDto.SupplyStatusId);
            supply.Supplier = supplier;
            supply.Sum = supplyDto.SupplyDetails.Sum(d => d.Amount * d.PricePerUnit);

            supply.SupplyDetails.Clear();
            foreach (var detailDto in supplyDto.SupplyDetails)
            {
                var detail = _mapper.Map<SupplyDetail>(detailDto);
                var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductName == detailDto.ProductName);
                detail.Product = product;
                detail.ShipmentInvoices.Clear();
                foreach (var invoiceDto in detailDto.ShipmentInvoices)
                {
                    var invoice = _mapper.Map<ShipmentInvoice>(invoiceDto);
                    
                    var employeeNames = invoiceDto.EmployeeName.Split(' ');
                    var employeeFirstName = employeeNames[0];
                    var employeeLastName = employeeNames.Length > 1 ? employeeNames[1] : "";
                    var employee = await _context.Employees.Include(e => e.Store).FirstOrDefaultAsync(e => e.FirstName == employeeFirstName && e.LastName == employeeLastName);

                    if (employee == null)
                    {
                        throw new Exception("Employee not found");
                    }

                    var store = await _context.Stores.FirstOrDefaultAsync(s => s.StoreName == employee.Store.StoreName);
                    if (store == null)
                    {
                        throw new Exception("Store not found");
                    }

                    var availableProduct = await _context.AvailableProducts
                        .FirstOrDefaultAsync(ap => ap.Store == store && ap.Product == detail.Product);

                    if (availableProduct == null)
                    {
                        availableProduct = new AvailableProduct
                        {
                            StoreId = store.StoreId,
                            ProductId = detail.Product.ProductId,
                            Quantity = (int?)invoice.TotalAmount
                        };
                        _context.AvailableProducts.Add(availableProduct);
                        await _context.SaveChangesAsync();
                    }

                    invoice.AvailableProduct = availableProduct;
                    invoice.Employee = employee;
                    detail.ShipmentInvoices.Add(invoice);
                }
                supply.SupplyDetails.Add(detail);
            }

            _context.Supplies.Add(supply);
            await _context.SaveChangesAsync();

            return _mapper.Map<SupplyDTO>(supply);
        }


        public async Task<SupplyDTO> UpdateSupplyAsync(int supplyId, SupplyDTO supplyDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteSupplyAsync(int supplyId)
        {
            
            var supply = await _context.Supplies
            .Include(s => s.SupplyDetails)
            .ThenInclude(sd => sd.ShipmentInvoices)
            .FirstOrDefaultAsync(s => s.SupplyId == supplyId);

            if (supply == null)
                return false;

            var details = await _context.SupplyDetails
                .Where(sd => sd.SupplyId == supplyId)
                .ToListAsync();

            if (details.Any())
            {
                var detailsIds = details.Select(d => d.SupplyDetailsId).ToList();

                var shipments = await _context.ShipmentInvoices
                    .Where(s => detailsIds.Contains(s.SupplyDetailsId ?? 0))
                    .ToListAsync();

                if (shipments.Any())
                {
                    _context.ShipmentInvoices.RemoveRange(shipments);
                }

                _context.SupplyDetails.RemoveRange(details);
            }

            _context.Supplies.Remove(supply);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<SupplyDTO>> GetAllSuppliesAsync()
        {
            var supplies = await _context.Supplies
            .Include(s => s.Supplier)
            .Include(s => s.SupplyDetails)
            .ThenInclude(sd => sd.Product)
            .Include(s => s.SupplyDetails)
            .ThenInclude(sd => sd.ShipmentInvoices)
            .ThenInclude(si => si.AvailableProduct)
            .ThenInclude(ap => ap.Store)
            .ThenInclude(s => s.City)
            .Include(s => s.SupplyDetails)
            .ThenInclude(sd => sd.ShipmentInvoices)
            .ThenInclude(si => si.Employee)
            .Include(s => s.Status)
            .ToListAsync();
            return _mapper.Map<IEnumerable<SupplyDTO>>(supplies);
        }

        public async Task<SupplyDTO> GetSupplyByIdAsync(int supplyId)
        {
            var supply = await _context.Supplies
            .Include(s => s.Supplier)
            .Include(s => s.SupplyDetails)
            .ThenInclude(sd => sd.Product)
            .Include(s => s.SupplyDetails)
            .ThenInclude(sd => sd.ShipmentInvoices)
            .ThenInclude(si => si.AvailableProduct)
            .ThenInclude(ap => ap.Store)
            .ThenInclude(s => s.City)
            .Include(s => s.SupplyDetails)
            .ThenInclude(sd => sd.ShipmentInvoices)
            .ThenInclude(si => si.Employee)
            .Include(s => s.Status)
            .FirstOrDefaultAsync(s => s.SupplyId == supplyId);
            return _mapper.Map<SupplyDTO>(supply);
        }

        public async Task<IEnumerable<SupplierDTO>> GetSuppliersAsync()
        {
            var suppliers = await _context.Suppliers
            .Include(s => s.City)
            .ToListAsync();

            return _mapper.Map<IEnumerable<SupplierDTO>>(suppliers);
        }

        public async Task<List<SupplyStatusDTO>> GetAllSupplyStatusesAsync()
        {
            var supplystatuses = await _context.SupplyStatuses
                .Select(c => _mapper.Map<SupplyStatusDTO>(c))
                .ToListAsync();

            return supplystatuses;
        }

        public async Task<SupplierDTO> GetSupplierByIdAsync(int supplierId)
        {
            var supplier = await _context.Suppliers
            .Include(s => s.City)
            .FirstOrDefaultAsync(s => s.SupplierId == supplierId);

            return _mapper.Map<SupplierDTO>(supplier);
        }

        public async Task<IEnumerable<SupplyDetailsDTO>> GetSupplyDetailsAsync(int supplyId)
        {
            var supplyDetails = await _context.SupplyDetails
            .Include(sd => sd.Product)
            .Include(sd => sd.ShipmentInvoices)
            .ThenInclude(si => si.AvailableProduct)
            .ThenInclude(ap => ap.Store)
            .Include(sd => sd.ShipmentInvoices)
            .ThenInclude(si => si.Employee)
            .Include(sd => sd.ShipmentInvoices)
            .Where(sd => sd.SupplyId == supplyId)
            .ToListAsync();

            return _mapper.Map<IEnumerable<SupplyDetailsDTO>>(supplyDetails);
        }

        public async Task<IEnumerable<ShipmentInvoiceDTO>> GetShipmentInvoicesAsync(int supplyDetailsId)
        {
            var shipmentInvoices = await _context.ShipmentInvoices
            .Include(sd => sd.AvailableProduct)
            .ThenInclude(sd => sd.Store)
            .Include(sd => sd.Employee)
            .Where(sd => sd.SupplyDetailsId == supplyDetailsId)
            .ToListAsync();

            return _mapper.Map<IEnumerable<ShipmentInvoiceDTO>>(shipmentInvoices);
        }

        public async Task<bool> IsSupplyNumberUniqueAsync(string supplyNumber)
        {
            return !await _context.Supplies.AnyAsync(s => s.SupplyNumber == supplyNumber);
        }

        public async Task<IEnumerable<StoreDTO>> GetStoresForEmployeeAsync(int employeeId)//в який магазин здійснено ShipmentInvoice залежно від працівника який її оформив та товару в SupplyDetails
        {
            throw new NotImplementedException();
        }
    }
}
