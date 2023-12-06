using SODP.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace SODP.Domain.Entities;

public sealed class Branch : ActiveStatusEntity, IOrdered
{
    public Sign Sign{ get; private set; }
    public Title Title { get; private set; }
    public int Order { get; private set; }
    public ICollection<BranchLicense> Licenses { get; set; }

	private Branch() { }

    private Branch(string sign, string title)
    {
        Sign = sign;
		Title = title;
        Order = 0;
    }

	public override string ToString()
    {
        return $"{Sign.Value.Trim()} {Title.Value.Trim()}";
    }

	public static Branch Create(string sign, string title)
	{
		return new Branch(sign, title);
	}

	public void SetSign(string sign)
	{
		Sign = sign;
	}

	public void SetTitle(string title)
	{
		Title = title;
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
