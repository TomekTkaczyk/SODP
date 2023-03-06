using SODP.Domain.Exceptions;

namespace SODP.Application.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException() : base("Entity not found.") { }
    }
}
