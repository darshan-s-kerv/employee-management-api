using EmployeeManagementApi.DTO;

namespace EmployeeManagementApi.Services.Interfaces;

public interface IAuthService
{
    public Task RegisterUser(RegisterUserRequest registerUserRequest);
    public Task<string> LoginUser(LoginUserRequest loginUserRequest);
}
