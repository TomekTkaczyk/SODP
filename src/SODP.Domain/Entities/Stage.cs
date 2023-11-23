
using SODP.Domain.ValueObjects;

namespace SODP.Domain.Entities;

public sealed class Stage : ActiveStatusEntity, IOrdered
{
	public Sign Sign { get; private set; }
	public string Title { get; private set; }
	public int Order { get; private set; }

    public Stage()
    {
        Order = 0;
    }

    public Stage(string sign, string title) : this()
	{
		Sign = sign;
		Title = title;
	}

	public static Stage Create(string sign, string title)
    {
        return new Stage(sign, title);
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
		return $"{Sign.Value.Trim()} {Title.Trim()}";
	}

	public void Up()
	{
		throw new System.NotImplementedException();
	}

	public void Down()
	{
		throw new System.NotImplementedException();
	}
}
