using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;
        public EmployeeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddEmployeeAsync(Employee employee)
        {
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            Employee? employee = await _dbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee {id} not found!");
            }
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _dbContext.Employees.ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _dbContext.Employees.FindAsync(id);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            await _dbContext.SaveChangesAsync();
        }
    }
}
