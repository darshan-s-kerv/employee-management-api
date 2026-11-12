using EmployeeManagementApi.DTO;
using EmployeeManagementApi.Entities;
using EmployeeManagementApi.Exceptions;
using EmployeeManagementApi.Repository.Interfaces;
using EmployeeManagementApi.Services.Interfaces;

namespace EmployeeManagementApi.Services;

public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
{
    private readonly IEmployeeRepository employeeRepository = employeeRepository;

    public async Task<List<Employee>> GetAllEmployees()
    {
        return await employeeRepository.GetAllAsync();
    }

    public async Task<Employee> GetEmployeeById(Guid id)
    {
        return await employeeRepository.GetByIdAsync(id) ?? throw new ApiException(404, $"Employee with ID {id} does not exist");
    }

    public async Task AddEmployee(Employee newEmployee)
    {
        var employee = await employeeRepository.GetByEmailAsync(newEmployee.Email);

        if(employee != null)
        {
            throw new ApiException(409, $"Employee with email {newEmployee.Email} already exists");
        }

        await employeeRepository.AddAsync(newEmployee);
    }

    public async Task UpdateEmployee(Guid id, Employee updatedEmployee)
    {
        await employeeRepository.UpdateAsync(await GetEmployeeById(id), updatedEmployee);
    }

    public async Task DeleteEmployee(Guid id)
    {
        await employeeRepository.DeleteAsync(await GetEmployeeById(id));
    }

}