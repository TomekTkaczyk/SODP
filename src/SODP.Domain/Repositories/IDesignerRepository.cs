using SODP.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace SODP.Domain.Repositories;

public interface IDesignerRepository : IPageRepository<Designer>
{
    Task<Designer> GetDetailsAsync(int id, CancellationToken cancellationToken);
}
