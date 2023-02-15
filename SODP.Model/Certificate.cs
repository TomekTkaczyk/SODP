using System;

namespace SODP.Model;

public class Certificate : BaseEntity
{
	public int DesignerId { get; set; }
	public virtual Designer Designer { get; set; }
	public string Number { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
}
