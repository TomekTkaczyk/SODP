namespace SODP.Domain.Exceptions;

public class ConflictException : DomainException
{
	public ConflictException(string entityName) : base($"{entityName} already exist.") { }
}
