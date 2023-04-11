using SODP.Shared.Extensions;

namespace SODP.Domain.Entities;

public class Stage : ActiveStatusEntity, IOrdered
{
	private Stage(string sign, string name)
    {
        Sign = sign;
        Name = name;
    }

    public int Order { get; set; }
    public string Sign { get; set; }
    public string Name { get; set; }

    public void Normalize()
    {
        Sign = Sign.ToUpper();
        Name = Name.CapitalizeFirstLetter();
    }

	public override string ToString()
	{
		return $"{Sign.Trim()} {Name.Trim()}";
	}

    public static Stage Create(string sign, string name)
    {
        return new Stage(sign, name);
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
