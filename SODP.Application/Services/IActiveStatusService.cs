using SODP.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface IActiveStatusService
    {
        Task<ServiceResponse> SetActiveStatusAsync(int id, bool status);
    }
}
