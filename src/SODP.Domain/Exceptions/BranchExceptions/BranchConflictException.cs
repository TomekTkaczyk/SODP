namespace SODP.Domain.Exceptions.BranchExceptions;

public class BranchConflictException : DomainException
{
    public BranchConflictException() : base("Branch already exist.") { }
}
