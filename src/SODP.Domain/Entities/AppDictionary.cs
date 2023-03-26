using System;
using System.Collections.Generic;

namespace SODP.Domain.Entities;

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

	bool IActiveStatus.ActiveStatus => throw new NotImplementedException();

	public bool Equals(AppDictionary other)
    {
        return this.Sign.Equals(other.Sign) && this.ParentId.Equals(other.ParentId);
    }

	public void SetActiveStatus(bool activeStatus)
	{
		ActiveStatus = activeStatus;
	}
}
