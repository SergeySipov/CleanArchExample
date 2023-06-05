using Domain.Common;

namespace Domain.Entities;

public class Student : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }

    public int StudentContractId { get; set; }
    public StudentContract Contract { get; set; }

    public int GroupId { get; set; }
    public Group Group { get; set; }
}
