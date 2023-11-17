namespace SODP.Domain.Exceptions.ProjectExceptions;

public sealed class FailProjectFolderException	: DomainException
{
    public FailProjectFolderException(string message) : base($"Project folder fail: {message}") { }
}
