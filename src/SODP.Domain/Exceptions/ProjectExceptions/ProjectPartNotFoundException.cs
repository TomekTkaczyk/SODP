namespace SODP.Domain.Exceptions.ProjectExceptions;

public class ProjectPartNotFoundException : DomainException
{
    public ProjectPartNotFoundException() : base("Project part not found.") { }
}
