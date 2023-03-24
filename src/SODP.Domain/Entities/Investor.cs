namespace SODP.Domain.Entities;

public class Investor : BaseEntity, IActiveStatus
{
    public string Name { get; set; }
    public bool? ActiveStatus { get; set; }
}
