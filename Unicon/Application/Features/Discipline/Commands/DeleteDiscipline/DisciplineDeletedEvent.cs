using Domain.Common;

namespace Application.Features.Discipline.Commands.DeleteDiscipline;

public class DisciplineDeletedEvent : BaseEvent
{
    public Domain.Entities.Discipline Discipline { get; }

    public DisciplineDeletedEvent(Domain.Entities.Discipline discipline)
    {
        Discipline = discipline;
    }
}
