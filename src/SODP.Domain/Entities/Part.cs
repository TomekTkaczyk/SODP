using SODP.Domain.ValueObjects;

namespace SODP.Domain.Entities;

public sealed class Part : ActiveStatusEntity, IOrdered
{
	public Sign Sign { get; private set; }

	public string Title { get; private set; }

	public int Order { get; private set; }
	
	private Part() { }

	private Part(string sign, string title)
    {
		Sign = sign.ToUpper();
		Title = title.ToUpper();
		Order = 1;
	}

	public static Part Create(string sign, string title)
	{
		return new Part(sign,title);
	}

	public void SetTitle(string title)
	{
		Title = title.ToUpper();
	}

	public override string ToString()
	{
		return $"{Sign.Value.Trim()} {Title.Trim()}";
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
