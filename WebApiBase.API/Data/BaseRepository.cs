using Microsoft.EntityFrameworkCore;

namespace WebApiBase.Data;

public class BaseRepository<T>(AppDbContext context) : IBaseRepository<T> where T : class
{

    public async Task<IEnumerable<T>> GetPaginateAsync(int pageNumber, int pageSize)
    {
        return await context.Set<T>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
    }


    public async Task<int> Update(T entity)
    {
        context.Set<T>().Update(entity);
        var saveChangesAsync = await context.SaveChangesAsync();
        return saveChangesAsync;
    }

    public void Delete(T entity)
    {
        context.Set<T>().Remove(entity);
    }
}