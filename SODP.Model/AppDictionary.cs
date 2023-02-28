using SODP.Model.Interfaces;
using System;
using System.Collections.Generic;

namespace SODP.Model;

public class AppDictionary : BaseEntity, IActiveStatus, IEquatable<AppDictionary>
{
	public AppDictionary()
	{
		Sign = "";
		Name = "";
		ActiveStatus = true;
	}

    public int? ParentId { get; set; }
    public AppDictionary Parent { get; set; }


    public string Sign { get; set; }
    public string Name { get; set; }
    public bool? ActiveStatus { get; set; }

    public virtual ICollection<AppDictionary> Children { get; set; }

    public bool Equals(AppDictionary other)
    {
        return this.Sign.Equals(other.Sign) && this.ParentId.Equals(other.ParentId);
    }
}
