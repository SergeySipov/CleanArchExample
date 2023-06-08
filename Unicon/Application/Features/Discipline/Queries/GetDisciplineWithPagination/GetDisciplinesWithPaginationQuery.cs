using Application.Common.DataModels;
using Application.Common.Models;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Discipline.Queries.GetDisciplineWithPagination;

public record GetDisciplinesWithPaginationQuery : IRequest<PaginatedList<DisciplineBriefDto>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}

internal class GetDisciplinesWithPaginationQueryHandler : IRequestHandler<GetDisciplinesWithPaginationQuery,
        PaginatedList<DisciplineBriefDto>>
{
    private readonly IMapper _mapper;
    private readonly IPaginatedRepository<Domain.Entities.Discipline> _disciplineRepository;

    public GetDisciplinesWithPaginationQueryHandler(IMapper mapper,
        IPaginatedRepository<Domain.Entities.Discipline> disciplineRepository)
    {
        _mapper = mapper;
        _disciplineRepository = disciplineRepository;
    }

    public async Task<PaginatedList<DisciplineBriefDto>> Handle(GetDisciplinesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var disciplinesAsyncEnumerable = _disciplineRepository.GetPaginatedChunkAsync<DisciplineBriefDataModel>(request.PageNumber, request.PageSize);

        var disciplines = await disciplinesAsyncEnumerable.ToListAsync(cancellationToken: cancellationToken);
        var disciplineBriefDtos = _mapper.Map<List<DisciplineBriefDto>>(disciplines);

        var totalItemsCount = await _disciplineRepository.CountAsync();

        var paginatedModel = new PaginatedList<DisciplineBriefDto>(disciplineBriefDtos, totalItemsCount,
            request.PageNumber, request.PageSize);

        return paginatedModel;
    }
}