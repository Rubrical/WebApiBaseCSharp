using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApiBase.Data;

namespace WebApiBase.IoC;

public class DependencyContainer
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
        });

    }
}