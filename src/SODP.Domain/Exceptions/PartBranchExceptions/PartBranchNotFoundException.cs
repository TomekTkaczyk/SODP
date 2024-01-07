namespace SODP.Domain.Exceptions.PartBranchExceptions;

public class PartBranchNotFoundException : DomainException
{
	public PartBranchNotFoundException() : base("Branch of part not fund") { }
}
