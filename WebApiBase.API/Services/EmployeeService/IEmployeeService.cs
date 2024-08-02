using WebApiBase.Models;

namespace WebApiBase.Services.EmployeeService;

public interface IEmployeeService
{
    public Task<ServiceResponse<IEnumerable<EmployeeModel>>> GetPaginateAsync(int pageNumber, int pageSize);
    public Task<ServiceResponse<EmployeeModel>> CreateNewAsync(EmployeeModel employeeModel);
    public Task<ServiceResponse<EmployeeModel>> GetByIdAsync(int id);
    public Task<ServiceResponse<EmployeeModel>> UpdateAsync(EmployeeModel editedEmployee);
    public Task<ServiceResponse<List<EmployeeModel>>> DeleteAsync(int id);
    public Task<ServiceResponse<EmployeeModel>> InactivateAsync(int id);
}