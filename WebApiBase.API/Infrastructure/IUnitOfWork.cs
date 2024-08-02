using WebApiBase.Data;
using WebApiBase.Models;

namespace WebApiBase.Infrastructure;

public interface IUnitOfWork
{ 
    IBaseRepository<EmployeeModel> EmployeesRepository { get; } 
    Task<int> CompleteAsync();
}