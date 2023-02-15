using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
	public interface IDictionaryService : IGetEntityService<DictionaryDTO>, IActiveStatusService { }
}
