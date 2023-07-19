using Domain.Common.Interfaces;

namespace Application.Interfaces.Repositories;

public interface IPaginatedRepository<T> where T : class, IEntity
{
    IAsyncEnumerable<TDataModel> GetPaginatedChunkAsync<TDataModel>(int pageNumber, int pageSize);
    Task<int> GetTotalNumberOfEntitiesAsync(CancellationToken cancellationToken);
}