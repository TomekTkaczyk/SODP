namespace SODP.Domain.Exceptions.InvestorExceptions;

public class InvestorConflictException : DomainException
{
    public InvestorConflictException() : base("Investor already exist.") { }
}
