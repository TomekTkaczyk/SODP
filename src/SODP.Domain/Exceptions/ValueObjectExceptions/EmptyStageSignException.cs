namespace SODP.Domain.Exceptions.ValueObjectExceptions;

public class EmptyStageSignException : DomainException
{
    public EmptyStageSignException() : base("Project stage sign is empty.") { }
}
