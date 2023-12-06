using SODP.Domain.ValueObjects;

namespace SODP.Domain.Entities;

public sealed class Stage : ActiveStatusEntity, IOrdered
{
	public Sign Sign { get; private set; }
	
	public Title Title { get; private set; }

	public int Order { get; private set; }

	private Stage() { }

    private Stage(string sign, string title)
	{
		Sign = sign;
		Title = title;
		Order = 1;
	}

	public static Stage Create(string sign, string title)
    {
        return new Stage(sign, title);
    }

	public void SetTitle(string title) 
	{ 
		Title = title; 
	}

	public override string ToString()
	{
		return $"{Sign.Value.Trim()} {Title.Value.Trim()}";
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
