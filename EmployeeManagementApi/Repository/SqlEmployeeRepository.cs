using EmployeeManagementApi.Data;
using EmployeeManagementApi.Entities;
using EmployeeManagementApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApi.Repository
{
    public class SqlEmployeeRepository(AppDbContext dbContext) : IEmployeeRepository
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task AddAsync(Employee employee)
        {
            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            dbContext.Employees.Remove(employee);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await dbContext.Employees.ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(Guid id)
        {
            return await dbContext.Employees.FindAsync(id);
        }

        public async Task<Employee?> GetByEmailAsync(string email)
        {
            return await dbContext.Employees
                .FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task UpdateAsync(Employee existingEmployee, Employee updatedEmployee)
        {
            existingEmployee.Name = updatedEmployee.Name;
            existingEmployee.Email = updatedEmployee.Email;
            existingEmployee.Department = updatedEmployee.Department;
            existingEmployee.Salary = updatedEmployee.Salary;

            await dbContext.SaveChangesAsync();
        }
    }
}
