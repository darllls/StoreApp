using AutoMapper;
using Business.Repository.IRepository;
using DataContextWH.Data;
using DataContextWH.Models;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class OrderDetailsFactRepository : IOrderDetailsFactRepository
    {
        private readonly StoreWhContext _context;
        private readonly IMapper _mapper;

        public OrderDetailsFactRepository(StoreWhContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDetailsFactDTO>> GetAllOrderDetailsFactsAsync()
        {
            var orderDetailsFacts = await _context.OrderDetailsFacts
                .Include(odf => odf.Product)
                .Include(odf => odf.Employee)
                .Include(odf => odf.CustomerType)
                .Include(odf => odf.PeriodStart)
                .Include(odf => odf.PeriodEnd)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderDetailsFactDTO>>(orderDetailsFacts);
        }

        public async Task<OrderDetailsFactDTO> GetOrderDetailsFactByIdAsync(int id)
        {
            var orderDetailsFact = await _context.OrderDetailsFacts
                .Include(odf => odf.Product)
                .Include(odf => odf.Employee)
                .Include(odf => odf.CustomerType)
                .Include(odf => odf.PeriodStart)
                .Include(odf => odf.PeriodEnd)
                .FirstOrDefaultAsync(odf => odf.OrderDetailsId == id);

            return _mapper.Map<OrderDetailsFactDTO>(orderDetailsFact);
        }
    }
}
