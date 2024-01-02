namespace SODP.Domain.Exceptions.ProjectExceptions;

public class PartBranchNotFoundException : DomainException
{
    public PartBranchNotFoundException() : base("Branch of part not foud.") { }
}
