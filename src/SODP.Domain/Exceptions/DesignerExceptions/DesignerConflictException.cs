namespace SODP.Domain.Exceptions.DesignerExceptions;

public class DesignerConflictException : DomainException
{
    public DesignerConflictException() : base("Designer already exist.") { }
}
