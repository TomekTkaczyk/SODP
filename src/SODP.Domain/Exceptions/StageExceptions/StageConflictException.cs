namespace SODP.Domain.Exceptions.StageExceptions;

public class StageConflictException	: ConflictException
{
    public StageConflictException() : base("Stage already exists.") { }
}
