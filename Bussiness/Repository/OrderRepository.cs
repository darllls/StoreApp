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
}
