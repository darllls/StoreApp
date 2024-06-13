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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeRepository(StoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EmployeeDTO>> GetAllEmployeesAsync()
        {
            // Отримання списку всіх співробітників з включеними даними про магазин і місто
            var employees = await _context.Employees
                .Include(e => e.Store)
                    .ThenInclude(s => s.City)
                .ToListAsync();

            // Мапування списку співробітників у список DTO
            var employeeDTOs = _mapper.Map<List<EmployeeDTO>>(employees);

            // Заповнення StoreName та CityName у кожному DTO
            foreach (var employeeDto in employeeDTOs)
            {
                if (employeeDto.StoreId != null)
                {
                    var store = employees.FirstOrDefault(e => e.EmployeeId == employeeDto.EmployeeId)?.Store;
                    if (store != null)
                    {
                        employeeDto.StoreName = store.StoreName;
                        if (store.City != null)
                        {
                            employeeDto.CityName = store.City.Name;
                        }
                    }
                }
            }

            return employeeDTOs;
        }


        public async Task<EmployeeDTO> GetEmployeeByIdAsync(int employeeId)
        {
            var employee = await _context.Employees
                .Include(e => e.Store)
                .ThenInclude(s => s.City)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            return _mapper.Map<EmployeeDTO>(employee);
        }

        public async Task<EmployeeDTO> CreateEmployeeAsync(EmployeeDTO employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);

            if (employeeDto.StoreId != null)
            {
                var store = await _context.Stores.Include(s => s.City).FirstOrDefaultAsync(s => s.StoreId == employeeDto.StoreId);
                if (store != null)
                {
                    employee.Store = store;

                    employeeDto.StoreName = store.StoreName;
                    if (store.City != null)
                    {
                        employeeDto.CityName = store.City.Name;
                    }
                }
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeDTO>(employee);
        }


        public async Task<EmployeeDTO> UpdateEmployeeAsync(int employeeId, EmployeeDTO employeeDto)
        {
            var existingEmployee = await _context.Employees.FindAsync(employeeId);
            if (existingEmployee == null)
                return null;

            _mapper.Map(employeeDto, existingEmployee);

            if (employeeDto.StoreId != null)
            {
                var store = await _context.Stores.Include(s => s.City).FirstOrDefaultAsync(s => s.StoreId == employeeDto.StoreId);
                if (store != null)
                {
                    existingEmployee.Store = store;

                    employeeDto.StoreName = store.StoreName;
                    if (store.City != null)
                    {
                        employeeDto.CityName = store.City.Name;
                    }
                }
            }
            existingEmployee.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeDTO>(existingEmployee);
        }

        public async Task<bool> DeleteEmployeeAsync(int employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null)
                return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
