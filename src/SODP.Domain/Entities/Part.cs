using SODP.Shared.ValueObjects;

namespace SODP.Domain.Entities;

public sealed class Part : ActiveStatusEntity, IOrdered
{
	public string Sign { get; private set; }

	public string Title { get; private set; }

	public int Order { get; private set; }
	
	private Part(string sign, string title)
    {
		Sign = sign;
		Title = title;
		Order = 0;
	}

	public static Part Create(string sign, string title)
	{
		return new Part(sign,title);
	}

	public void SetSign(string sign)
	{
		Sign = sign;
	}

	public void SetTitle(string title)
	{
		Title = title;
	}

	public override string ToString()
	{
		return $"{Sign.Trim()} {Title.Trim()}";
	}

	public void Down()
	{
		throw new System.NotImplementedException();
	}

	public void Up()
	{
		throw new System.NotImplementedException();
	}
}
