using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Common.DataModels;

public record DisciplineBriefDataModel : IMapFrom<Discipline>
{
    public string Name { get; set; }
    public int NumberOfHours { get; set; }
}
