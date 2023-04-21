using Microsoft.EntityFrameworkCore;
using SODP.Shared.ValueObjects;
using System;
using System.Collections.Generic;

namespace SODP.Domain.Entities;

public class Branch : ActiveStatusEntity, IOrdered
{
    public string Sign{ get; set; }
    public string Title { get; set; }
    public int Order { get; set; } = 0;
    public virtual ICollection<BranchLicense> Licenses { get; set; }


    private Branch(string sign, string title)
    {
        Sign = sign;
		Title = title;
        Order = 0;
    }

	public override string ToString()
    {
        return $"{Sign.Trim()} {Title.Trim()}";
    }

	public static Branch Create(string sign, string title)
	{
		return new Branch(sign, title);
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
