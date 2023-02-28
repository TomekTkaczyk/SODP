using SODP.Model.Interfaces;

namespace SODP.Model;

public class Investor : BaseEntity, IActiveStatus
{
    public string Name { get; set; }
    public bool? ActiveStatus { get; set; }
}
