namespace SODP.Domain.Exceptions.DesignerExceptions;

public class DesignerNotFoundException : DomainException
{
    public DesignerNotFoundException() : base("Designer not found") { }
}
