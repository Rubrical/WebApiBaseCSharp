using WebApiBase.Enums;

namespace WebApiBase.Data.DTOs;

public class EmployeeDTO
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public DepartmentEnum Department { get; set; }
    public ShiftEnum Shift { get; set; }
}