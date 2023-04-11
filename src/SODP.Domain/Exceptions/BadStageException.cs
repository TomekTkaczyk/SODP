namespace SODP.Domain.Exceptions;

public class BadStageException : DomainException
{
	public BadStageException(string message) : base(message) { }
}
