using SODP.Application.Interfaces;
using System;

namespace SODP.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
