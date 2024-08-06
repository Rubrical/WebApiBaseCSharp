using WebApiBase.Data.DTOs;
using WebApiBase.Models;

namespace WebApiBase.Services.EmployeeService;

public interface IEmployeeService
{
    public Task<ServiceResponse<IEnumerable<EmployeeModel>>> GetPaginateAsync(int pageNumber, int pageSize);
    public Task<ServiceResponse<EmployeeModel>> CreateNewAsync(EmployeeModel newEmployee);
    public Task<ServiceResponse<EmployeeModel>> GetByIdAsync(int id);
    public Task<ServiceResponse<EmployeeModel>> UpdateAsync(EditedEmployeeDto editedEmployee);
    public Task<ServiceResponse<IEnumerable<EmployeeModel>>> DeleteAsync(int id);
    public Task<ServiceResponse<EmployeeModel>> InactivateAsync(int id);
}