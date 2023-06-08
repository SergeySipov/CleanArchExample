using Domain.Common.Interfaces;

namespace Application.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class, IEntity
{
    ValueTask<T> GetByIdAsync(int id);
    IAsyncEnumerable<T> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task DeleteByIdAsync(int id);
}