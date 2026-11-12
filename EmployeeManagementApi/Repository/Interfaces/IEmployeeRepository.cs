using EmployeeManagementApi.Entities;

namespace EmployeeManagementApi.Repository.Interfaces;

public interface IEmployeeRepository
{
    public Task<List<Employee>> GetAllAsync();

    public Task<Employee?> GetByIdAsync(Guid id);

    public Task<Employee?> GetByEmailAsync(string email);

    public Task AddAsync(Employee employee);

    public Task UpdateAsync(Employee existingEmployee, Employee updatedEmployee);

    public Task DeleteAsync(Employee employee);
}