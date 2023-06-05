using Domain.Common;

namespace Domain.Entities;

public class Group : BaseEntity
{
    public string Name { get; set; }

    public int DepartmentId { get; set; }
    public Department Department { get; set; }
}
