namespace SODP.Domain.Exceptions.LicenseExceptions;

internal class LicenceBranchConflictException : DomainException
{
	public LicenceBranchConflictException() : base("License already have a branch") { }
}
