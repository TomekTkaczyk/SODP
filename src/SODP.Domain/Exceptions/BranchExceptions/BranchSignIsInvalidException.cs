namespace SODP.Domain.Exceptions.PartExceptions;

public class BranchSignIsInvalidException : DomainException
{
    public BranchSignIsInvalidException() : base("Branch sign is invalid.") { }
}
