namespace SODP.Domain.Exceptions.StageExceptions;

public sealed class StageNotFoundException	: DomainException
{
    public StageNotFoundException() : base("Stage not found.") { }
}
