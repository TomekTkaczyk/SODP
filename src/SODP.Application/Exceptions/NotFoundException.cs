using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODP.Application.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException() : base("Entity not found.") { }
    }
}
