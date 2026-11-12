using EmployeeManagementApi.DTO;
using EmployeeManagementApi.Entities;

namespace EmployeeManagementApi.Services.Interfaces;

public interface IEmployeeService
{
    public Task<List<Employee>> GetAllEmployees();
    public Task<Employee> GetEmployeeById(Guid id);
    public Task AddEmployee(Employee newEmployee);
    public Task UpdateEmployee(Guid id, Employee updatedEmployee);
    public Task DeleteEmployee(Guid id);
}