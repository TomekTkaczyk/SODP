namespace SODP.Domain.Exceptions;

public class UnknowDeleteException : DomainException
{
	public UnknowDeleteException(string message) : base(message) { }
}
