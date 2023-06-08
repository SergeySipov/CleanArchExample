using Domain.Common;

namespace Application.Features.Discipline.Commands.CreateDiscipline;

public class DisciplineCreatedEvent : BaseEvent
{
    public Domain.Entities.Discipline Discipline { get; }

    public DisciplineCreatedEvent(Domain.Entities.Discipline discipline)
    {
        Discipline = discipline;
    }
}
