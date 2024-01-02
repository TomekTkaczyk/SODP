namespace SODP.Domain.Exceptions.ProjecPartExceptions;

public class PartBranchConflictException : DomainException
{
	public PartBranchConflictException() : base("Part branch already exist.") { }
}
