namespace SODP.Domain.Exceptions.ProjectExceptions;

public class ProjectPartConflictException : DomainException
{
    public ProjectPartConflictException() : base("Project part already exist.") { }
}
