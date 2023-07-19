using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Discipline.Commands.DeleteDiscipline;

public record DeleteDisciplineCommand : IRequest
{
    public int DisciplineId { get; set; }
}

internal class DeleteDisciplineCommandHandler : IRequestHandler<DeleteDisciplineCommand>
{
    private readonly IBaseRepository<Domain.Entities.Discipline> _baseRepository;

    public DeleteDisciplineCommandHandler(IBaseRepository<Domain.Entities.Discipline> baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public Task Handle(DeleteDisciplineCommand request, CancellationToken cancellationToken)
    {
        return _baseRepository.DeleteByIdAsync(request.DisciplineId);
    }
}