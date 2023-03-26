using SODP.Shared.Response;

namespace SODP.Domain.Entities;

public class Investor : ActivatedEntity
{
    private Investor(string name)
    {
        Name = name;
    }

    public string Name { get; set; }

    public static Investor Create(string name)
    {
        return new Investor(name);
    }
}
