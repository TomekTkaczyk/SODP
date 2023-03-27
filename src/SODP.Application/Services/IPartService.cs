using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
	public interface IPartService : IEntityService<PartDTO>, IActiveStatusService
	{
		Task<ServicePageResponse<PartDTO>> GetPageAsync(bool? active, string searchString, int pageNumber, int pageSize);
		Task<ServiceResponse<PartDTO>> GetAsync(string sign);
	}
}
