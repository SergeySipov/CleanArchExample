namespace Domain.Entities;

public class TimeTable : BaseAuditableEntity
{
    public DateTime DateTime { get; set; }
    public DayOfWeek DayOfWeek { get; set; }

    public int GroupId { get; set; }
    public Group Group { get; set; }

    public int DisciplineId { get; set; }
    public Discipline Discipline { get; set; }

    public int AuditoryId { get; set; }
    public Auditory Auditory { get; set; }

    public int EducatorId { get; set; }
    public Educator Educator { get; set; }

    public int DispatcherId { get; set; }
    public Dispatcher Dispatcher { get; set; }
}
