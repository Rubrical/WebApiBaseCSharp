using System.ComponentModel.DataAnnotations;
using WebApiBase.Enums;

namespace WebApiBase.Models
{
    public class EmployeeModel
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public DepartmentEnum Department { get; set; }
        public ShiftEnum Shift { get; set; }
        public bool Active { get; set; } = true;
        public DateTime DateCreation { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}