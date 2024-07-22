using Microsoft.EntityFrameworkCore;

namespace WebApiBase.Data;

public class BaseRepository<T>(AppDbContext context) : IBaseRepository<T> where T : class
{
    protected readonly AppDbContext Context = context;

    public async Task<IEnumerable<T>> GetPaginateAsync(int pageNumber, int pageSize)
    {
        return await Context.Set<T>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await Context.Set<T>().FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
    }

    public void Update(T entity)
    {
        Context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        Context.Set<T>().Remove(entity);
    }
}