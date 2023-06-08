using Application.Interfaces.UnitOfWork;
using Persistence.DbContexts;

namespace Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly UniconDbContext _dbContext;

    public UnitOfWork(UniconDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task RollbackAsync()
    {
        _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        return Task.CompletedTask;
    }

    public Task<int> SaveAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
