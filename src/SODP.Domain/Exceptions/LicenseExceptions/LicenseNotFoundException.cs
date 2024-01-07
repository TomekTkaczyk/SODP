namespace SODP.Domain.Exceptions.LicenseExceptions;

public class LicenseNotFoundException : DomainException
{
	public LicenseNotFoundException() : base("License not found") { }
}
