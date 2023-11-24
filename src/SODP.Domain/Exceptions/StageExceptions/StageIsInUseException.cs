namespace SODP.Domain.Exceptions.StageExceptions;

public class StageIsInUseException : DomainException
{
    public StageIsInUseException() : base("Stage is in use.") { }
}
