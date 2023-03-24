namespace SODP.Domain.Exceptions.Stages
{
    public class StageNotFoundException : AppException
    {
        public StageNotFoundException() : base("Stage not found.") { }
    }
}
