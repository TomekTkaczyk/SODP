namespace SODP.Domain.Exceptions.PartExceptions;

public sealed class PartIsNullException : DomainException
{
    public PartIsNullException() : base("Part is null.") { }
}
