using AutoMapper;
using EmployeeManagementApi.DTO;
using EmployeeManagementApi.Entities;

namespace EmployeeManagementApi.Mappings;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Employee, EmployeeResponse>();
        CreateMap<CreateEmployeeRequest, Employee>()
            .ConstructUsing(request => new Employee(
                Guid.NewGuid(),
                request.Name,
                request.Email,
                request.Department,
                request.Salary
            ));
        CreateMap<UpdateEmployeeRequest, Employee>();
    }
}
