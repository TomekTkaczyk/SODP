namespace SODP.Domain.Exceptions;

public class DesignerConflictException : DomainException
{
	public DesignerConflictException() : base($"Designer already exist.")
	{
	}
}
