using AutoMapper;
using Business.Repository.IRepository;
using DataContext.Data;
using DataContext.Models;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(StoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _context.ProductCategories
                .Select(c => _mapper.Map<CategoryDTO>(c))
                .ToListAsync();

            return categories;
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _context.ProductCategories
                .FirstOrDefaultAsync(c => c.CategoryId == categoryId);

            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
