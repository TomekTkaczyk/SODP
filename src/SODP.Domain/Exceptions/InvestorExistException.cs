namespace SODP.Domain.Exceptions;

public class InvestorExistException : DomainException
{
	public InvestorExistException() : base("Investor already exist.") { }
}
