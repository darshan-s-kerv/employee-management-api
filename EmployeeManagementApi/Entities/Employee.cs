using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace EmployeeManagementApi.Entities;

public class Employee
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Department { get; set; }

    public int Salary { get; set; }

    public Employee()
    {
        
    }

    public Employee(Guid id, string name, string email, string department, int salary)
    {
        Id = id;
        Name = name;
        Department = department;
        Salary = salary;
        Email = email;
    }
}
