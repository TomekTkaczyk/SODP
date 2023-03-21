using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface ISearchStringService
    {
        IAppService WithSearchString(string searchString);
    }
}
