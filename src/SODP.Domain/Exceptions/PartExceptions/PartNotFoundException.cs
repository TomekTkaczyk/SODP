namespace SODP.Domain.Exceptions.PartExceptions;

public sealed class PartNotFoundException : NotFoundException
{
    public PartNotFoundException() : base("Part not found.") { }
}
