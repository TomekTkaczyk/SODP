namespace SODP.Domain.Exceptions;

public class NotFoundException : DomainException
    {
	public NotFoundException(string entityName) : base($"{entityName} not found.") { }
}
