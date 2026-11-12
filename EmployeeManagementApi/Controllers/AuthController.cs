using EmployeeManagementApi.DTO;
using EmployeeManagementApi.Filters;
using EmployeeManagementApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }


    [HttpPost]
    [Route("Register")]
    [ValidateDTO]
    public async Task<IActionResult> Register(
    [FromBody] RegisterUserRequest registerUserRequest)
    {
        await authService.RegisterUser(registerUserRequest);

        return StatusCode(StatusCodes.Status201Created, new
        {
            Message = "User registered successfully"
        });
    }

    [HttpPost]
    [Route("Login")]
    [ValidateDTO]
    public async Task<IActionResult> Login(
    [FromBody] LoginUserRequest loginUserRequest)
    {
        var token = await authService.LoginUser(loginUserRequest);

        return Ok(new
        {
            token
        });
    }
}
