using SODP.Shared.ValueObjects;

namespace SODP.Domain.Entities;

public class Part : ActiveStatusEntity, IOrdered
{
	public string Sign { get; set; }

	public string Title { get; set; }

	public int Order { get; private set; }
	
	public Part() : this("", "") { }

	public Part(string sign) : this(sign, "") { }

	public Part(string sign = "", string title = "")
    {
		Sign = sign;
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
