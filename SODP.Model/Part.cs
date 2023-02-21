using SODP.Model.Extensions;
using SODP.Model.Interfaces;

namespace SODP.Model;

public class Part : BaseEntity, IActiveStatus, IOrdered
{
	public Part() : this("", "") { }

	public Part(string sign) : this(sign, "") { }

	public Part(string sign = "", string name = "")
    {
        Sign = sign;
        Name = name;
    }

    public string Sign { get; set; }
    public string Name { get; set; }
    public bool ActiveStatus { get; set; }
    public int Order { get; set; }

    public void Normalize()
    {
        Sign = Sign.ToUpper();
        Name = Name.CapitalizeFirstLetter();
    }

    public override string ToString()
    {
        return $"{ Sign.Trim()} {Name.Trim()}";
    }
}
