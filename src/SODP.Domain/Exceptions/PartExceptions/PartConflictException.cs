namespace SODP.Domain.Exceptions.PartExceptions;

public class PartConflictException : ConflictException
{
	public PartConflictException() : base("Part sign already exists.") { }
}
