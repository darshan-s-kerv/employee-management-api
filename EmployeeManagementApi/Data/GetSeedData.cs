using EmployeeManagementApi.Entities;
using System.Text.Json;

namespace EmployeeManagementApi.Data;

public class SeedData
{
    public static List<Employee> Get()
    {
        var filePath = Path.Combine(
            AppContext.BaseDirectory,
            "Data",
            "EmployeesData.json"
        );

        var json = File.ReadAllText(filePath);

        return JsonSerializer.Deserialize<List<Employee>>(json) ?? [];
    }
}