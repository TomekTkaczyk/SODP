using SODP.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace SODP.Domain.Repositories;

public interface IProjectPartRepository : IRepository<ProjectPart> 
{
	Task<ProjectPart> GetWithDetailsAsync(int id, CancellationToken cancellationToken);
}
