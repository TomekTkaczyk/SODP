using SODP.Domain.ValueObjects;
using System;

namespace SODP.Domain.Entities;

public class Certificate : BaseEntity
{
	public int DesignerId { get; set; }
	public virtual Designer Designer { get; set; }
	public CertificateNumber Number { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
}
