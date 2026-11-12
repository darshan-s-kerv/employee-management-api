using EmployeeManagementApi.Data;
using EmployeeManagementApi.Entities;
using EmployeeManagementApi.Repository.Interfaces;

namespace EmployeeManagementApi.Repository;

public class InMemoryEmployeeRepository : IEmployeeRepository
{
    private readonly List<Employee> employees;

    public InMemoryEmployeeRepository()
    {
        employees = SeedData.Get();
    }

    public Task<List<Employee>> GetAllAsync()
    {
        return Task.FromResult(employees);
    }

    public Task AddAsync(Employee employee)
    {
        employees.Add(employee);

        return Task.CompletedTask;
    }

    public Task<Employee?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(employees.FirstOrDefault(e => e.Id == id));
    }

    public Task<Employee?> GetByEmailAsync(string email)
    {
        return Task.FromResult(employees.FirstOrDefault(e => e.Email == email));
    }

    public Task UpdateAsync(Employee existingEmployee, Employee updatedEmployee)
    {
        existingEmployee.Name = updatedEmployee.Name;
        existingEmployee.Email = updatedEmployee.Email;
        existingEmployee.Department = updatedEmployee.Department;
        existingEmployee.Salary = updatedEmployee.Salary;

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Employee employee)
    {
        employees.Remove(employee);

        return Task.CompletedTask;
    }
}