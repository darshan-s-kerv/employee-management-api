using EmployeeManagementApi.Data;
using EmployeeManagementApi.Entities;
using EmployeeManagementApi.Exceptions;
using EmployeeManagementApi.Repository.Interfaces;
using EmployeeManagementApi.Services;
using Moq;

namespace EmployeeManagementApi.Tests.Services;

public class EmployeeServiceTests
{
    private readonly List<Employee> employees;
    private readonly Mock<IEmployeeRepository> mockRepository;

    public EmployeeServiceTests()
    {
        employees = SeedData.Get();
        mockRepository = new Mock<IEmployeeRepository>();
    }

    [Fact]
    public async Task GetAllEmployees_ReturnsAllEmployees()
    {
        mockRepository
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(employees);

        var employeeService = new EmployeeService(mockRepository.Object);

        var result = await employeeService.GetAllEmployees();

        Assert.NotNull(result);
        Assert.Equal(employees, result);

        mockRepository.Verify(
            repo => repo.GetAllAsync(),
            Times.Once
        );
    }


    [Fact]
    public async Task GetEmployeeById_ReturnsEmployee_WhenEmployeeExists()
    {
        var employee = employees[0];

        mockRepository
            .Setup(repo => repo.GetByIdAsync(employee.Id))
            .ReturnsAsync(employee);

        var employeeService = new EmployeeService(mockRepository.Object);

        var result = await employeeService.GetEmployeeById(employee.Id);

        Assert.NotNull(result);
        Assert.Equal(employee.Id, result.Id);
        Assert.Equal(employee.Email, result.Email);
        Assert.Equal(employee.Name, result.Name);
        Assert.Equal(employee.Salary, result.Salary);

        mockRepository.Verify(
            repo => repo.GetByIdAsync(employee.Id),
            Times.Once
        );
    }

    [Fact]
    public async Task GetEmployeeById_ThrowsApiException_WhenEmployeeDoesNotExist()
    {
        var id = Guid.NewGuid();

        mockRepository
            .Setup(repo => repo.GetByIdAsync(id))
            .ReturnsAsync((Employee?)null);

        var employeeService = new EmployeeService(mockRepository.Object);

        var exception = await Assert.ThrowsAsync<ApiException>(
            () => employeeService.GetEmployeeById(id)
        );

        Assert.Contains(id.ToString(), exception.Message);

        mockRepository.Verify(
            repo => repo.GetByIdAsync(id),
            Times.Once
        );
    }


    [Fact]
    public async Task AddEmployee_ThrowsApiException_WhenEmailAlreadyExists()
    {
        var newEmployee = employees[0];

        mockRepository
            .Setup(repo => repo.GetByEmailAsync(newEmployee.Email))
            .ReturnsAsync(employees[0]);

        var employeeService = new EmployeeService(mockRepository.Object);

        var exception = await Assert.ThrowsAsync<ApiException>(
            () => employeeService.AddEmployee(newEmployee)
        );

        Assert.Contains(newEmployee.Email, exception.Message);

        mockRepository.Verify(
            repo => repo.AddAsync(It.IsAny<Employee>()),
            Times.Never
        );
    }

    [Fact]
    public async Task AddEmployee_AddsEmployee_WhenEmailDoesNotExist()
    {
        var newEmployee = employees[0];

        mockRepository
            .Setup(repo => repo.GetByEmailAsync(newEmployee.Email))
            .ReturnsAsync((Employee?)null);

        var employeeService = new EmployeeService(mockRepository.Object);

        await employeeService.AddEmployee(newEmployee);

        mockRepository.Verify(
            repo => repo.GetByEmailAsync(newEmployee.Email),
            Times.Once
        );

        mockRepository.Verify(
            repo => repo.AddAsync(newEmployee),
            Times.Once
        );
    }


    [Fact]
    public async Task UpdateEmployee_UpdatesEmployee_WhenEmployeeExists()
    {
        var existingEmployee = employees[0];
        var updatedEmployee = employees[1];

        mockRepository
            .Setup(repo => repo.GetByIdAsync(existingEmployee.Id))
            .ReturnsAsync(existingEmployee);

        var employeeService = new EmployeeService(mockRepository.Object);

        await employeeService.UpdateEmployee(existingEmployee.Id, updatedEmployee);

        mockRepository.Verify(
            repo => repo.UpdateAsync(existingEmployee, updatedEmployee),
            Times.Once
        );
    }

    [Fact]
    public async Task UpdateEmployee_ThrowsApiException_WhenEmployeeDoesNotExist()
    {
        var id = Guid.NewGuid();
        var updatedEmployee = employees[0];

        mockRepository
            .Setup(repo => repo.GetByIdAsync(id))
            .ReturnsAsync((Employee?)null);

        var employeeService = new EmployeeService(mockRepository.Object);

        await Assert.ThrowsAsync<ApiException>(
            () => employeeService.UpdateEmployee(id, updatedEmployee)
        );

        mockRepository.Verify(
            repo => repo.UpdateAsync(
                It.IsAny<Employee>(),
                It.IsAny<Employee>()
            ),
            Times.Never
        );
    }


    [Fact]
    public async Task DeleteEmployee_DeletesEmployee_WhenEmployeeExists()
    {
        var employee = employees[0];

        mockRepository
            .Setup(repo => repo.GetByIdAsync(employee.Id))
            .ReturnsAsync(employee);

        var employeeService = new EmployeeService(mockRepository.Object);

        await employeeService.DeleteEmployee(employee.Id);

        mockRepository.Verify(
            repo => repo.DeleteAsync(employee),
            Times.Once
        );
    }

    [Fact]
    public async Task DeleteEmployee_ThrowsApiException_WhenEmployeeDoesNotExist()
    {
        var id = Guid.NewGuid();

        mockRepository
            .Setup(repo => repo.GetByIdAsync(id))
            .ReturnsAsync((Employee?)null);

        var employeeService = new EmployeeService(mockRepository.Object);

        await Assert.ThrowsAsync<ApiException>(
            () => employeeService.DeleteEmployee(id)
        );

        mockRepository.Verify(
            repo => repo.DeleteAsync(It.IsAny<Employee>()),
            Times.Never
        );
    }

}
