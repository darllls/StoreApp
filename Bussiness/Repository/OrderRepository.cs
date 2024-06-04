using AutoMapper;
using DataContext.Data;
using DataContext.Models;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Bussiness.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class OrderRepository : IOrderRepository
{
    private readonly StoreDbContext _context;
    private readonly IMapper _mapper;

    public OrderRepository(StoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
    {
        var orders = await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Employee).ThenInclude(e => e.Store).ThenInclude(s => s.City)
            .Include(o => o.OrderItems).ThenInclude(oi => oi.AvailableProduct).ThenInclude(ap => ap.Product)
            .ToListAsync();

        return _mapper.Map<IEnumerable<OrderDTO>>(orders);
    }

    public async Task<OrderDTO> GetOrderByIdAsync(int orderId)
    {
        var order = await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Employee).ThenInclude(e => e.Store).ThenInclude(s => s.City)
            .Include(o => o.OrderItems).ThenInclude(oi => oi.AvailableProduct).ThenInclude(ap => ap.Product)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);

        return _mapper.Map<OrderDTO>(order);
    }

    public async Task<OrderDTO> CreateOrderAsync(OrderDTO orderDto)
    {
        var order = _mapper.Map<Order>(orderDto);

        // Retrieve Employee details based on the provided name
        var employeeNames = orderDto.EmployeeName.Split(' ');
        var employeeFirstName = employeeNames[0];
        var employeeLastName = employeeNames.Length > 1 ? employeeNames[1] : "";
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.FirstName == employeeFirstName && e.LastName == employeeLastName);

        // Retrieve Customer details based on the provided name
        var customerNames = orderDto.CustomerName.Split(' ');
        var customerFirstName = customerNames[0];
        var customerLastName = customerNames.Length > 1 ? customerNames[1] : "";
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.FirstName == customerFirstName && c.LastName == customerLastName);

        if (employee != null && customer != null)
        {
            // Set EmployeeId and CustomerId on the order
            order.EmployeeId = employee.EmployeeId;
            order.CustomerId = customer.CustomerId;

            // Fetch product prices and calculate TotalAmount based on OrderItems
            decimal? totalAmount = 0;

            // Clear existing order items to avoid duplication
            order.OrderItems.Clear();

            foreach (var orderItemDto in orderDto.OrderItems)
            {
                var availableProduct = await _context.AvailableProducts
                                                      .Include(ap => ap.Product)
                                                      .FirstOrDefaultAsync(ap => ap.Product.ProductName == orderItemDto.ProductName && ap.StoreId == employee.StoreId);

                if (availableProduct != null)
                {
                    totalAmount += orderItemDto.Amount * availableProduct.Product.Price;

                    // Set the AvailableProductID for each OrderItem
                    var orderItem = _mapper.Map<OrderItem>(orderItemDto);
                    orderItem.AvailableProductId = availableProduct.AvailableProductId;
                    order.OrderItems.Add(orderItem);
                }
            }
            order.TotalAmount = totalAmount;

            // Fetch Store details from Employee and set StoreName and CityName
            if (employee.StoreId != null)
            {
                var store = await _context.Stores.Include(s => s.City).FirstOrDefaultAsync(s => s.StoreId == employee.StoreId);
                if (store != null)
                {
                    orderDto.StoreName = store.StoreName;
                    if (store.City != null)
                    {
                        orderDto.CityName = store.City.Name;
                    }
                }
            }
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return _mapper.Map<OrderDTO>(order);
    }

    public async Task<OrderDTO> UpdateOrderAsync(int orderId, OrderDTO orderDto)
    {
        var existingOrder = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (existingOrder == null)
            return null;

        _mapper.Map(orderDto, existingOrder);

        // Retrieve Employee details based on the provided name
        var employeeNames = orderDto.EmployeeName.Split(' ');
        var employeeFirstName = employeeNames[0];
        var employeeLastName = employeeNames.Length > 1 ? employeeNames[1] : "";
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.FirstName == employeeFirstName && e.LastName == employeeLastName);

        // Retrieve Customer details based on the provided name
        var customerNames = orderDto.CustomerName.Split(' ');
        var customerFirstName = customerNames[0];
        var customerLastName = customerNames.Length > 1 ? customerNames[1] : "";
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.FirstName == customerFirstName && c.LastName == customerLastName);

        if (employee != null && customer != null)
        {
            // Set EmployeeId and CustomerId on the existing order
            existingOrder.EmployeeId = employee.EmployeeId;
            existingOrder.CustomerId = customer.CustomerId;

            existingOrder.OrderItems.Clear();

            // Fetch product prices and calculate TotalAmount based on OrderItems
            decimal? totalAmount = 0;
            foreach (var orderItemDto in orderDto.OrderItems)
            {
                var availableProduct = await _context.AvailableProducts
                                                      .Include(ap => ap.Product)
                                                      .FirstOrDefaultAsync(ap => ap.Product.ProductName == orderItemDto.ProductName && ap.StoreId == employee.StoreId);

                if (availableProduct != null)
                {
                    totalAmount += orderItemDto.Amount * availableProduct.Product.Price;

                    // Set the AvailableProductID for each OrderItem
                    var orderItem = _mapper.Map<OrderItem>(orderItemDto);
                    orderItem.AvailableProductId = availableProduct.AvailableProductId;
                    existingOrder.OrderItems.Add(orderItem);
                }
            }
            existingOrder.TotalAmount = totalAmount;

            // Fetch Store details from Employee and set StoreName and CityName
            if (employee.StoreId != null)
            {
                var store = await _context.Stores.Include(s => s.City).FirstOrDefaultAsync(s => s.StoreId == employee.StoreId);
                if (store != null)
                {
                    orderDto.StoreName = store.StoreName;
                    if (store.City != null)
                    {
                        orderDto.CityName = store.City.Name;
                    }
                }
            }
        }

        await _context.SaveChangesAsync();
        return _mapper.Map<OrderDTO>(existingOrder);
    }



    public async Task<bool> DeleteOrderAsync(int orderId)
    {
        var existingOrder = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (existingOrder == null)
            return false;

        // Remove associated OrderItems
        _context.OrderItems.RemoveRange(existingOrder.OrderItems);

        // Remove the order itself
        _context.Orders.Remove(existingOrder);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<OrderItemDTO>> GetOrderItemsAsync(int orderId)
    {
        var orderItems = await _context.OrderItems
            .Include(oi => oi.AvailableProduct)
            .ThenInclude(ap => ap.Product)
            .Where(oi => oi.OrderId == orderId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<OrderItemDTO>>(orderItems);
    }

    public async Task<bool> IsOrderNumberUniqueAsync(string orderNumber)
    {
        return !await _context.Orders.AnyAsync(o => o.Number == orderNumber);
    }

    public async Task<IEnumerable<AvailableProductDTO>> GetAvailableProductsForEmployeeStoreAsync(int employeeId)
    {
        var employee = await _context.Employees.Include(e => e.Store)
                                               .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

        if (employee == null) return Enumerable.Empty<AvailableProductDTO>();

        var availableProducts = await _context.AvailableProducts
                                              .Include(ap => ap.Product)
                                              .Where(ap => ap.StoreId == employee.StoreId)
                                              .ToListAsync();

        var availableProductDTOs = new List<AvailableProductDTO>();

        foreach (var availableProduct in availableProducts)
        {
            var availableProductDTO = _mapper.Map<AvailableProductDTO>(availableProduct);

            // Fetch the product name from the associated ProductDTO
            availableProductDTO.ProductName = availableProduct.Product.ProductName;

            availableProductDTOs.Add(availableProductDTO);
        }

        return availableProductDTOs;
    }
}
