using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApi.DTO;

public class CreateEmployeeRequest
{
    [Required]
    [MinLength(2, ErrorMessage = "Name must be atleast 2 characters")]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "Department must be atleast 2 characters")]
    public string Department { get; set; }

    [Required]
    [Range(10000, int.MaxValue, ErrorMessage = "Salary must be atleast 10000")]
    public int Salary { get; set; }
}
