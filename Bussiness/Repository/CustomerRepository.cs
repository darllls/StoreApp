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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;

        public CustomerRepository(StoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers
                .Include(c => c.CustomerType)
                .Select(c => _mapper.Map<CustomerDTO>(c))
                .ToListAsync();

            return customers;
        }

        public async Task<CustomerDTO> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _context.Customers
                .Include(c => c.CustomerType)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            //if (customer == null)
            //{
            //    throw new NotFoundException($"Customer with ID {customerId} not found.");
            //}

            return _mapper.Map<CustomerDTO>(customer);
        }

        public async Task<CustomerDTO> CreateCustomerAsync(CustomerDTO customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);

            customer.CustomerType = await _context.CustomerTypes.FindAsync(customerDto.CustomerTypeId);

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerDTO>(customer);
        }

        public async Task<CustomerDTO> UpdateCustomerAsync(int customerId, CustomerDTO customerDto)
        {
            var existingCustomer = await _context.Customers.FindAsync(customerId);
            if (existingCustomer == null)
                return null;

            _mapper.Map(customerDto, existingCustomer);

            existingCustomer.CustomerType = await _context.CustomerTypes.FindAsync(customerDto.CustomerTypeId);

            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerDTO>(existingCustomer);
        }

        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
                return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return true;
        }

    }

}
