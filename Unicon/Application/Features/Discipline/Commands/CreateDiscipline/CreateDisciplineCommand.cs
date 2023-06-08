using Application.Common.Mappings;
using Application.Common.Models;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using MediatR;

namespace Application.Features.Discipline.Commands.CreateDiscipline;

public record CreateDisciplineCommand : IRequest<Result>, IMapFrom<Domain.Entities.Discipline>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int NumberOfHours { get; set; }
}

internal class CreateDisciplineCommandHandler : IRequestHandler<CreateDisciplineCommand, Result>
{
    private readonly IBaseRepository<Domain.Entities.Discipline> _disciplineRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateDisciplineCommandHandler(IBaseRepository<Domain.Entities.Discipline> disciplineRepository, 
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _disciplineRepository = disciplineRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result> Handle(CreateDisciplineCommand request, CancellationToken cancellationToken)
    {
        var discipline = _mapper.Map<Domain.Entities.Discipline>(request);

        await _disciplineRepository.AddAsync(discipline);

        discipline.AddDomainEvent(new DisciplineCreatedEvent(discipline));

        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}