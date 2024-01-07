namespace SODP.Domain.Exceptions.ProjectExceptions;

public class LicenceAlreadyUsedException : DomainException
{
	public LicenceAlreadyUsedException() : base("License already used") { }
}