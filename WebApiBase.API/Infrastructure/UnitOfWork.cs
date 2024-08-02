using WebApiBase.Data;
using WebApiBase.Models;

namespace WebApiBase.Infrastructure;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _context;
    private BaseRepository<EmployeeModel> employeeRepository = null;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IBaseRepository<EmployeeModel> EmployeesRepository
    {
        get
        {
            if (employeeRepository == null)
            {
                employeeRepository = new BaseRepository<EmployeeModel>(_context);
            }

            return employeeRepository;
        }
    }
    

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}