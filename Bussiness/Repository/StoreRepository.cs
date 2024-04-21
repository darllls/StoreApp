using AutoMapper;
using Bussiness.Repository.IRepository;
using DataContext.Data;
using DataContext.Models;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;

        public StoreRepository(StoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<StoreDTO>> GetAllStoresAsync()
        {
            var stores = await _context.Stores
                .Include(e => e.City)
                .ToListAsync();

            var storeDTOs = _mapper.Map<List<StoreDTO>>(stores);

            foreach (var storeDto in storeDTOs)
            {
                if (storeDto.CityId != null)
                {
                    var city = stores.FirstOrDefault(e => e.StoreId == storeDto.StoreId)?.City;
                    if (city != null)
                    {
                        storeDto.CityName = city.Name;

                    }
                }
            }

            return storeDTOs;
        }

        public async Task<StoreDTO> GetStoreByIdAsync(int storeId)
        {
            var store = await _context.Stores
                .Include(e => e.City)
                .FirstOrDefaultAsync(e => e.StoreId == storeId);

            //if (employee == null)
            //{
            //    throw new NotFoundException($"Employee with ID {employeeId} not found.");
            //}

            return _mapper.Map<StoreDTO>(store);
        }
    }
}
