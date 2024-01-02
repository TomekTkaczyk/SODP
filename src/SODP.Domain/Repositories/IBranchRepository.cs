using SODP.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace SODP.Domain.Repositories;

public interface IBranchRepository : IPageRepository<Branch>
{
    Task<Branch> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken);
}
