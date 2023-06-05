using Domain.Common;

namespace Domain.Entities;

public class Department : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public int FaculityId { get; set; }
    public Faculity Faculity { get; set; }
}
