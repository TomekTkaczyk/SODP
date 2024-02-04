namespace SODP.Domain.Exceptions.ProjectExceptions;

public class ProjectPartConflictException : ConflictException
{
    public ProjectPartConflictException() : base("Project part already exist.") { }
}
