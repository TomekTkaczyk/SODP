using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface IInvestorService : IEntityService<InvestorDTO>, IActiveStatusService 
    {
        Task<ServicePageResponse<InvestorDTO>> GetPageAsync(bool? active, int currentPage, int pageSize, string searchString);
    }
}
