using WebApiBase.Enums;

namespace WebApiBase.Data.DTOs;

public class EditedEmployeeDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public DepartmentEnum Department { get; set; }
    public ShiftEnum Shift { get; set; }
}