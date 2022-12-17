using SODP.Shared.Interfaces;
using System;

namespace SODP.Shared.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
