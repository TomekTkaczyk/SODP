using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SODP.Domain.Services
{
    public interface ILicenseService : IEntityService<LicenseDTO>
    {
        Task<ServiceResponse<LicenseWithBranchesDTO>> GetBranchesAsync(int id);

        Task<ServiceResponse> AddBranchAsync(int id, int branchId);

        Task<ServiceResponse> RemoveBranchAsync(int id, int branchId);
    }
}
