namespace SODP.Domain.Exceptions;

public class LicenseExistException : DomainException
{
	public LicenseExistException() : base($"License already exist") { }
}
