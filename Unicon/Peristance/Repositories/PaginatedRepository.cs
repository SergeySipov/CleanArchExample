using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public class PaginatedRepository<T> : IPaginatedRepository<T> where T : BaseEntity
{
    private readonly UniconDbContext _dbContext;
    private readonly IMapper _mapper;

    public PaginatedRepository(UniconDbContext uniconDbContext, 
        IMapper mapper)
    {
        _dbContext = uniconDbContext;
        _mapper = mapper;
    }

    public IAsyncEnumerable<TDataModel> GetPaginatedChunkAsync<TDataModel>(int pageNumber, int pageSize)
    {
        var items = _dbContext.Set<T>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        var mappedEnumerable = _mapper.ProjectTo<TDataModel>(items)
            .AsAsyncEnumerable();

        return mappedEnumerable;
    }

    public Task<int> GetTotalNumberOfEntitiesAsync(CancellationToken cancellationToken)
    {
        var countEntitiesTask = _dbContext.Set<T>().CountAsync(cancellationToken: cancellationToken);
        return countEntitiesTask;
    }
}
