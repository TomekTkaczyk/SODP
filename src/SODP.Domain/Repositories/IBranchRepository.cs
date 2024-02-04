using SODP.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace SODP.Domain.Repositories;

public interface IBranchRepository : IRepository<Branch>
{
    Task<Branch> GetDetailsAsync(int id, CancellationToken cancellationToken);
}
