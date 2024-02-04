namespace SODP.Domain.Exceptions.InvestorExceptions;

public class InvestorNotFoundException : DomainException
{
    public InvestorNotFoundException() : base("Investor not found.") { }
}
