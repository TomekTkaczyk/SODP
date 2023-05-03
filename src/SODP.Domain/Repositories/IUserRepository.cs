using SODP.Domain.Entities;
using SODP.Shared.Response;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Domain.Repositories;

public interface IUserRepository
{
	IQueryable<User> Users { get; }

	Task<Page<User>> GetPageAsync(
		bool? activeStatus,
		string searchString,
		int pageNumber,
		int pageSize,
		CancellationToken cancellationToken);
}
