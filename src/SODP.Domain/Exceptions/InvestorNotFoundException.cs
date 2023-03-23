namespace SODP.Domain.Exceptions
{
	public class InvestorNotFoundException : AppException
	{
		public InvestorNotFoundException() : base("Investor not found.")
		{
		}
	}
}
