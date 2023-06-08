namespace Application.Interfaces.DbContext;

public interface IUniconDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}