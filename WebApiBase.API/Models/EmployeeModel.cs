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
        public bool Active { get; set; }
        public string Shift { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime UpdateDate { get; set; } = DateTime.Now.ToLocalTime();
    }
}