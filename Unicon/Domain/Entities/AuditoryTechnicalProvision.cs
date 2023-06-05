using Domain.Common;

namespace Domain.Entities;

public class AuditoryTechnicalProvision : BaseEntity
{
    public int TechnicalProvisionId { get; set; }
    public TechnicalProvision TechnicalProvision { get; set; }

    public int AuditoryId { get; set; }
    public Auditory Auditory { get; set; }
}
