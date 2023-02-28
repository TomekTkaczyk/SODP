using SODP.Model.Extensions;
using SODP.Model.Interfaces;

namespace SODP.Model;

public class Stage : BaseEntity, IActiveStatus, IOrdered
{
    public Stage() : this("","") { }

	public Stage(string sign) : this(sign, "") { }

	public Stage(string sign, string name)
    {
        Sign = sign;
        Name = name;
    }

    public int Order { get; set; }
    public string Sign { get; set; }
    public string Name { get; set; }
	public bool? ActiveStatus { get; set; }

    public void Normalize()
    {
        Sign = Sign.ToUpper();
        Name = Name.CapitalizeFirstLetter();
    }

	public override string ToString()
	{
		return $"{Sign.Trim()} {Name.Trim()}";
	}
}
