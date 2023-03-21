using SODP.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODP.Domain.Exceptions
{
    internal class BadStageException : AppException
    {
        public BadStageException(string message) : base(message)
        {
        }
    }
}
