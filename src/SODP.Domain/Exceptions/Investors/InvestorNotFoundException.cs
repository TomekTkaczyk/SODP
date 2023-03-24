namespace SODP.Domain.Exceptions.Investors
{
    public class InvestorNotFoundException : AppException
    {
        public InvestorNotFoundException() : base("Investor not found.")
        {
        }
    }
}
