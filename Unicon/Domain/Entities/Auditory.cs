using Domain.Common;

namespace Domain.Entities;

public class Auditory : BaseEntity
{
    public string Address { get; set; }
    public int PossibleStudentsNumber { get; set; }
}
