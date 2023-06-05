using Domain.Common;

namespace Domain.Entities;

public class TechnicalProvision : BaseEntity
{
    public string Name { get; set; }
    public int InventoryNumber { get; set; }
}
