using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApi.DTO;

public class RegisterUserRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }
}
