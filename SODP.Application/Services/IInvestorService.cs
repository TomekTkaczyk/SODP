using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public interface IInvestorService : IEntityService<InvestorDTO>
    {
        Task<ServicePageResponse<InvestorDTO>> GetAllAsync(int currentPage = 1, int pageSize = 0, bool? active = false);
        Task<int> GetAsync(InvestorDTO designer);
        Task<ServiceResponse> SetActiveStatusAsync(int id, bool status);
    }
}
