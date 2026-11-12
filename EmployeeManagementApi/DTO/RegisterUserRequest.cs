using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApi.DTO;

public class RegisterUserRequest
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [MinLength(6)]
    public required string Password { get; set; }
}
