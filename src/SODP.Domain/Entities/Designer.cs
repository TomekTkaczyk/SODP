using Microsoft.EntityFrameworkCore.Internal;
using SODP.Domain.Exceptions;
using SODP.Domain.ValueObjects;
using SODP.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace SODP.Domain.Entities;

public class Designer : ActiveStatusEntity
{
    public DesignerTitle Title { get;  set; }
    public FirstName Firstname { get;  set; }
    public LastName Lastname { get;  set; }
    public virtual ICollection<License> Licenses { get;  set; } = new List<License>();

    private Designer() { }

    private Designer(string title, string firstName, string lastName)
    {
        Title = title;
        Firstname = firstName;
        Lastname = lastName;
    }

    public static Designer Create(string title, string firstName, string lastName)
    {
        return new Designer(title, firstName, lastName)
        {
            Title = title,
            Firstname = firstName,
            Lastname = lastName
        };
    }

	public void SetName(string title, string firstName, string lastName)
    {
        Title = title;
        Firstname = firstName;
        Lastname = lastName;

        Normalize();
    }

    public License AddLicense(License license)
    {
		if (Licenses.Where(x => x.Content.ToUpper().Equals(license.Content.ToUpper())).Any())
        {
            throw new DesignerHaveLicenseException();
		}
        Licenses.Add(license);

        return license;
    }

    public void RemoveLicense(License license)
    {
        Licenses.Remove(license);
    }

    public override string ToString()
    {
        return Firstname.Value.Trim() + " " + Lastname.Value.Trim();
    }

    public void Normalize()
    {
        Firstname = Firstname.Value.CapitalizeFirstLetter();
        Lastname = Lastname.Value.CapitalizeFirstLetter();
    }
}
