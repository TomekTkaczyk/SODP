namespace SODP.Domain.Exceptions;

public class ResourceIsInUseException : DomainException
{
	public ResourceIsInUseException(string message) : base($"Resource {message} is in use.") { }
}
