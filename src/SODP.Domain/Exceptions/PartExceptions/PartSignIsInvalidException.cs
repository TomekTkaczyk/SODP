namespace SODP.Domain.Exceptions.PartExceptions;

public class PartSignIsInvalidException : DomainException
{
    public PartSignIsInvalidException() : base("Part sign is invalid.") { }
}
