using Application.Interfaces.Repositories;
using Domain.Common;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly UniconDbContext _dbContext;

    public BaseRepository(UniconDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        var exist = await _dbContext.Set<T>().FindAsync(entity.Id);
        _dbContext.Entry(exist).CurrentValues.SetValues(entity);
    }

    public Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public Task DeleteByIdAsync(int id)
    {
        var entity = (T)Activator.CreateInstance(typeof(T), id)!;

        var set = _dbContext.Set<T>();

        set.Attach(entity);
        set.Remove(entity);

        return _dbContext.SaveChangesAsync();
    }

    public IAsyncEnumerable<T> GetAllAsync()
    {
        return _dbContext
            .Set<T>()
            .AsAsyncEnumerable();
    }

    public ValueTask<T> GetByIdAsync(int id)
    {
        return _dbContext.Set<T>().FindAsync(id)!;
    }
}
