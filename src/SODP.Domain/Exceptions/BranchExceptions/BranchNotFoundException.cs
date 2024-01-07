namespace SODP.Domain.Exceptions.BranchExceptions;

public class BranchNotFoundException : DomainException
{
	public BranchNotFoundException() : base("Branch not found exception") { }
}
