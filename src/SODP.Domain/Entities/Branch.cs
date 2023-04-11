using SODP.Shared.Extensions;
using System;
using System.Collections.Generic;

namespace SODP.Domain.Entities;

public class Branch : ActiveStatusEntity, IOrdered
{
    public string Sign { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int Order { get; private set; } = 0;
    public virtual ICollection<BranchLicense> Licenses { get; set; }


    private Branch(string sign, string name)
    {
        Sign = sign;
        Name = name;
        Order = 0;

        Normalize();
    }


	public void Normalize()
    {
        Sign = Sign.ToUpper();
        Name = Name.CapitalizeFirstLetter();
    }


	public override string ToString()
    {
        return $"{ Sign.Trim()} {Name.Trim()}";
    }

    public void SetName(string name)
    {
        Name = name;
    }

	public static Branch Create(string sign, string name)
	{
        if(sign is null || string.IsNullOrWhiteSpace(sign))
        {
            throw new ArgumentException("Bad sign value.");
        }

		return new Branch(sign, name);
	}

	public void Up()
	{
		throw new NotImplementedException();
	}

	public void Down()
	{
		throw new NotImplementedException();
	}
}
