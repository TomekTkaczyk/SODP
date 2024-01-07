namespace SODP.Domain.Exceptions.LicenseExceptions;

public class LicenseContentExistException : DomainException
{
	public LicenseContentExistException() : base("License content exist") { }
}
