using System.Collections.Generic;
using SODP.Shared.Extensions;

namespace SODP.Domain.Entities;

public class Designer : ActiveStatusEntity
{
    public string Title { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public virtual ICollection<License> Licenses { get; set; }

    public override string ToString()
    {
        return Firstname.Trim() + " " + Lastname.Trim();
    }

    public void Normalize()
    {
        Firstname = Firstname.CapitalizeFirstLetter();
        Lastname = Lastname.CapitalizeFirstLetter();
    }
}
