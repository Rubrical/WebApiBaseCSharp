using Microsoft.EntityFrameworkCore;
using WebApiBase.Models;

namespace WebApiBase.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<EmployeeModel> Employees { get; private set; }
}