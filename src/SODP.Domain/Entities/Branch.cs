using SODP.Shared.Extensions;
using System;
using System.Collections.Generic;

namespace SODP.Domain.Entities;

public class Branch : ActiveStatusEntity, IOrdered
{
    public string Sign { get; private set; }
    public string Name { get; set; } = string.Empty;
    public int Order { get; set; } = 0;

    private Branch(string sign, string name)
    {
        Sign = sign;
        Name = name;
        Order = 0;
    }
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


	public static Branch Create(string sign, string name)
	{
        if(sign is null || string.IsNullOrWhiteSpace(sign))
        {
            throw new ArgumentException("Bad sign value.");
        }

		return new Branch(sign, name);
	}
}
