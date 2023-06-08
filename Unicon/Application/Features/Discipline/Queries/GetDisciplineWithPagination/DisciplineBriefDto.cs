using Application.Common.DataModels;
using Application.Common.Mappings;

namespace Application.Features.Discipline.Queries.GetDisciplineWithPagination;

public record DisciplineBriefDto : IMapFrom<DisciplineBriefDataModel>
{
    public string Name { get; set; }
    public int NumberOfHours { get; set; }
}
