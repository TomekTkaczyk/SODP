namespace SODP.Domain.Exceptions;

public class InvestorNotFoundException : DomainException
{
	public InvestorNotFoundException() : base("Investor not found.") { }
}
