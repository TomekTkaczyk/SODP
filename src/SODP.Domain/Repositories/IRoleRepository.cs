using SODP.Domain.Entities;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Domain.Repositories;

public interface IRoleRepository
{
	Task<Page<Role>> GetPageAsync(
		bool? activeStatus,
		string searchString,
		int pageNumber,
		int pageSize,
		CancellationToken cancellationToken);
}
