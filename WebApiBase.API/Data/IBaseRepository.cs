namespace WebApiBase.Data;

public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> GetPaginateAsync(int pageNumber, int pageSize);
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task<int> Update(T entity);
    void Delete(T entity);
}