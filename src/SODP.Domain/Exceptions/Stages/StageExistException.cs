namespace SODP.Domain.Exceptions.Stages
{
    public class StageExistException : AppException
    {
        public StageExistException() : base("Stage already exist.") { }
    }
}
