using WebApiBase.Data.Interfaces;
using WebApiBase.Models;

namespace WebApiBase.Data.Repositories;

public class EmployeeRepository(AppDbContext context) : BaseRepository<EmployeeModel>(context), IEmployeeRepository { }