namespace SODP.Domain.Exceptions.ProjectExceptions;

public class BranchRoleExistException : DomainException
{
	public BranchRoleExistException() : base("Role of branch exist") { }
}
