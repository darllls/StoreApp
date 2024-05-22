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
    public class BrandRepository : IBrandRepository
    {
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;

        public BrandRepository(StoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<BrandDTO>> GetAllBrandsAsync()
        {
            var brands = await _context.Brands
                .Select(c => _mapper.Map<BrandDTO>(c))
                .ToListAsync();

            return brands;
        }

        public async Task<BrandDTO> GetBrandByIdAsync(int brandId)
        {
            var brand = await _context.Brands
                .FirstOrDefaultAsync(c => c.BrandId == brandId);

            //if (brand == null)
            //{
            //    throw new NotFoundException($"Brand with ID {brandId} not found.");
            //}

            return _mapper.Map<BrandDTO>(brand);
        }
    }
}
