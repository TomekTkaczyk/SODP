namespace SODP.Domain.Exceptions.ProjectExceptions;

public class ProjectPartNotFoundException : DomainException
{
    public ProjectPartNotFoundException() : base("Part of project not found.") { }
}
