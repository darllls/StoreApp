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
    public class SupplyDetailsFactRepository : ISupplyDetailsFactRepository
    {
        private readonly StoreWhContext _context;
        private readonly IMapper _mapper;

        public SupplyDetailsFactRepository(StoreWhContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplyDetailsFactDTO>> GetAllSupplyDetailsFactsAsync()
        {
            var supplyDetailsFacts = await _context.SupplyDetailsFacts
                .Include(sdf => sdf.Supply)
                .ThenInclude(sdf => sdf.SupplyDate)
                .Include(sdf => sdf.Product)
                .ToListAsync();

            return _mapper.Map<IEnumerable<SupplyDetailsFactDTO>>(supplyDetailsFacts);
        }

        public async Task<SupplyDetailsFactDTO> GetSupplyDetailsFactByIdAsync(int id)
        {
            var supplyDetailsFact = await _context.SupplyDetailsFacts
                .Include(sdf => sdf.Supply)
                .ThenInclude(sdf => sdf.SupplyDate)
                .Include(sdf => sdf.Product)
                .FirstOrDefaultAsync(sdf => sdf.SupplyDetailsId == id);

            return _mapper.Map<SupplyDetailsFactDTO>(supplyDetailsFact);
        }
    }
}
