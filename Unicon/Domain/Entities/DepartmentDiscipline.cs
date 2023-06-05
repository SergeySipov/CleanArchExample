using Domain.Common;

namespace Domain.Entities;

public class DepartmentDiscipline : BaseEntity
{
    public int NumberOfHours { get; set; }

    public int DisciplineId { get; set; }
    public Discipline Discipline { get; set; }

    public int DepartmentId { get; set; }
    public Department Department { get; set; }
}
