using SODP.Model.Interfaces;
using System.Collections.Generic;

namespace SODP.Model;

public class AppDictionary : BaseEntity, IActiveStatus
{
	public string Master { get; set; }
	public string Sign { get; set; }
	public string Name { get; set; }
	public bool ActiveStatus { get; set; }
	public	ICollection<AppDictionary> Slaves { get; set; }
}
