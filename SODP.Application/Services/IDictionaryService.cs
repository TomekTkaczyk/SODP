using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
	public interface IDictionaryService : IEntityService<DictionaryDTO>, IActiveStatusService
	{
		Task<ServicePageResponse<DictionaryDTO>> GetPageAsync(bool? active, int currentPage = 1, int pageSize = 0, string searchString = "");
		Task<ServicePageResponse<DictionaryDTO>> GetPageAsync(string master, bool? active, int currentPage = 1, int pageSize = 0, string searchString = "");
		Task<ServiceResponse<DictionaryDTO>> GetAsync(string master, string sign);
		Task<ServiceResponse> DeleteAsync(string master, string sign);
	}
}
