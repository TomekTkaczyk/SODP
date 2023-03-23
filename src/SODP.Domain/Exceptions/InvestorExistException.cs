namespace SODP.Domain.Exceptions
{
	public class InvestorExistException : AppException
	{
		public InvestorExistException() : base("Investor already exist.") { }
	}
}
