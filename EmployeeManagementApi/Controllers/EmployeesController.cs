using EmployeeManagementApi.DTO;
using EmployeeManagementApi.Entities;
using EmployeeManagementApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementApi.Filters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService employeeService;
    private readonly IMapper mapper;

    public EmployeesController(IEmployeeService employeeService, IMapper mapper)
    {
        this.employeeService = employeeService;
        this.mapper = mapper;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> GetAll()
    {
        var employees = await employeeService.GetAllEmployees();
        var response = mapper.Map<List<EmployeeResponse>>(employees);

        return Ok(response);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var employee = await employeeService.GetEmployeeById(id);
        var response = mapper.Map<EmployeeResponse>(employee);

        return Ok(response);
    }

    [HttpPost]
    [ValidateDTO]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeRequest request)
    {
        var employee = mapper.Map<Employee>(request);
        await employeeService.AddEmployee(employee);

        return CreatedAtAction(
            nameof(GetById),
            new { id = employee.Id },
            employee
        );
    }

    [HttpPut]
    [Route("{id:guid}")]
    [ValidateDTO]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateEmployeeRequest updateEmployeeRequest)
    {
        var updatedEmployee = mapper.Map<Employee>(updateEmployeeRequest);
        await employeeService.UpdateEmployee(id, updatedEmployee);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await employeeService.DeleteEmployee(id);

        return NoContent();
    }
}
