using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public class PaginatedRepository<T> : IPaginatedRepository<T> where T : BaseEntity
{
    private readonly UniconDbContext _uniconDbContext;
    private readonly IMapper _mapper;

    public PaginatedRepository(UniconDbContext uniconDbContext, 
        IMapper mapper)
    {
        _uniconDbContext = uniconDbContext;
        _mapper = mapper;
    }

    public IAsyncEnumerable<TDataModel> GetPaginatedChunkAsync<TDataModel>(int pageNumber, int pageSize)
    {
        var items = _uniconDbContext.Set<T>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        var mappedEnumerable = _mapper.ProjectTo<TDataModel>(items)
            .AsAsyncEnumerable();

        return mappedEnumerable;
    }

    public Task<int> CountAsync()
    {
        var countEntitiesTask = _uniconDbContext.Set<T>().CountAsync();
        return countEntitiesTask;
    }
}
