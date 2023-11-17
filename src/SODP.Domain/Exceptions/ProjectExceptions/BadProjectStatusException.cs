namespace SODP.Domain.Exceptions.ProjectExceptions;

public sealed class BadProjectStatusException : DomainException
{
    public BadProjectStatusException() : base("Project status is invalid.") { }
}
