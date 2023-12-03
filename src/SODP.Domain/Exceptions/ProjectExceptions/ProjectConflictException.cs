namespace SODP.Domain.Exceptions.ProjectExceptions;

public class ProjectConflictException : ConflictException
{
	public ProjectConflictException() : base("Project already axist.") { }
}
