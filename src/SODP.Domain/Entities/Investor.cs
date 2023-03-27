namespace SODP.Domain.Entities;

public sealed class Investor : ActivatedEntity
{
    private Investor(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }

	public void SetName(string name)
	{
		Name = name;
	}
	
    public static Investor Create(string name)
    {
        return new Investor(name);
    }
}
