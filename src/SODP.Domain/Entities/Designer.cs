using Microsoft.EntityFrameworkCore.Internal;
using SODP.Domain.Exceptions;
using SODP.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace SODP.Domain.Entities;

public class Designer : ActiveStatusEntity
{
    public string Title { get;  set; }
    public string Firstname { get;  set; }
    public string Lastname { get;  set; }
    public virtual ICollection<License> Licenses { get;  set; } = new List<License>();

 //   public Designer(string title, string firstName, string lastName)
 //   {
	//	Title = title;
	//	Firstname = firstName;
	//	Lastname = lastName;
 //       //Licenses = new List<License>();
	//}

    public static Designer Create(string title, string firstName, string lastName)
    {
        return new Designer()
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
        return Firstname.Trim() + " " + Lastname.Trim();
    }

    public void Normalize()
    {
        Firstname = Firstname.CapitalizeFirstLetter();
        Lastname = Lastname.CapitalizeFirstLetter();
    }
}
