using System.ComponentModel.DataAnnotations;
using WebApiBase.Enums;

namespace WebApiBase.Models
{
    public class EmployeeModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DepartmentEnum Department { get; set; }
        public ShiftEnum Shift { get; set; }
        public bool Active { get; set; } = true;
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}