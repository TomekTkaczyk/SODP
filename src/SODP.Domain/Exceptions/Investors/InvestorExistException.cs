namespace SODP.Domain.Exceptions.Investors
{
    public class InvestorExistException : AppException
    {
        public InvestorExistException() : base("Investor already exist.") { }
    }
}
