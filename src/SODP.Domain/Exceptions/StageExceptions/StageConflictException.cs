namespace SODP.Domain.Exceptions.StageExceptions;

public class StageConflictException	: DomainException
{
    public StageConflictException() : base("Stage sign exist.") { }
}
