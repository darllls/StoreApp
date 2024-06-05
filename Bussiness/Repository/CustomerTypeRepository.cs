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
    public class CustomerTypeRepository : ICustomerTypeRepository
    {
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;

        public CustomerTypeRepository(StoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CustomerTypeDTO>> GetAllCustomerTypesAsync()
        {
            var customertypes = await _context.CustomerTypes
                .Select(c => _mapper.Map<CustomerTypeDTO>(c))
                .ToListAsync();

            return customertypes;
        }

        public async Task<CustomerTypeDTO> GetCustomerTypeByIdAsync(int customertypeId)
        {
            var customertype = await _context.CustomerTypes
                .FirstOrDefaultAsync(c => c.CustomerTypeId == customertypeId);

            //if (customertype == null)
            //{
            //    throw new NotFoundException($"CustomerType with ID {customertypeId} not found.");
            //}

            return _mapper.Map<CustomerTypeDTO>(customertype);
        }

        public async Task<CustomerTypeDTO> CreateCustomerTypeAsync(CustomerTypeDTO customertypeDto)
        {
            var customertype = _mapper.Map<CustomerType>(customertypeDto);

            _context.CustomerTypes.Add(customertype);
            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerTypeDTO>(customertype);
        }

        public async Task<CustomerTypeDTO> UpdateCustomerTypeAsync(int customertypeId, CustomerTypeDTO customertypeDto)
        {
            var existingCustomerType = await _context.CustomerTypes.FindAsync(customertypeId);
            if (existingCustomerType == null)
                return null;

            _mapper.Map(customertypeDto, existingCustomerType);

            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerTypeDTO>(existingCustomerType);
        }

        public async Task<bool> DeleteCustomerTypeAsync(int customertypeId)
        {
            var customertype = await _context.CustomerTypes.FindAsync(customertypeId);
            if (customertype == null)
                return false;

            _context.CustomerTypes.Remove(customertype);
            await _context.SaveChangesAsync();

            return true;
        }

    }

}
