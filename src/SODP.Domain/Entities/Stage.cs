using SODP.Shared.ValueObjects;

namespace SODP.Domain.Entities;

public class Stage : ActiveStatusEntity, IOrdered
{
	public string Sign { get; set; }
	public string Title { get; set; }
	public int Order { get; private set; }


	private Stage(string sign, string title)
	{
		Sign = sign;
		Title = title;
	}

	public static Stage Create(string sign, string title)
    {
        return new Stage(sign, title);
    }

	public override string ToString()
	{
		return $"{Sign.Trim()} {Title.Trim()}";
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
