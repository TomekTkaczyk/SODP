namespace SODP.Domain.Exceptions.StageExceptions;

public sealed class StageNotFoundException	: NotFoundException
{
    public StageNotFoundException() : base("Stage not found.") { }
}
