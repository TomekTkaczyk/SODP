using SODP.Model.Interfaces;
using System;
using System.Collections.Generic;

namespace SODP.Model;

public class AppDictionary : BaseEntity, IActiveStatus, IEquatable<AppDictionary>
{
	public AppDictionary()
	{
		Master = "";
		Sign = "";
		Name = "";
		ActiveStatus = true;
	}

	public string Master { get; set; }
	public string Sign { get; set; }
	public string Name { get; set; }
	public bool ActiveStatus { get; set; }
	public	ICollection<AppDictionary> Slaves { get; set; }
    public bool Equals(AppDictionary other)
    {
        return this.Sign.Equals(other.Sign) && this.Master.Equals(other.Master);
    }
}
