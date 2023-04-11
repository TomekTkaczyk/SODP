using SODP.Domain.Services;

namespace SODP.Infrastructure.Services
{
	public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
