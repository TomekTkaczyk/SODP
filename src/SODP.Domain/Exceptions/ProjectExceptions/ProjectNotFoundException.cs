namespace SODP.Domain.Exceptions.ProjectExceptions;

public class ProjectNotFoundException : DomainException
{
    public ProjectNotFoundException() : base("Project not found.") { }
}
