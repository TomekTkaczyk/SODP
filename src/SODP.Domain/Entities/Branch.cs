using SODP.Shared.Extensions;
using System.Collections.Generic;

namespace SODP.Domain.Entities;

public class Branch : ActivatedEntity, IOrdered
{
    public string Sign { get; set; }
    public string Name { get; set; }
	public int Order { get; set; }

	public virtual ICollection<BranchLicense> Licenses { get; set; }

	public void Normalize()
    {
        Sign = Sign.ToUpper();
        Name = Name.CapitalizeFirstLetter();
    }

	public override string ToString()
    {
        return $"{ Sign.Trim()} {Name.Trim()}";
    }
}
