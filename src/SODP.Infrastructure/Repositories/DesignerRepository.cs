using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;

namespace SODP.Infrastructure.Repositories;

internal class DesignerRepository : PagedRepository<Designer>, IDesignerRepository
{
	public DesignerRepository(SODPDBContext dbContext, ILogger<Designer> logger ) : base(dbContext, logger) { }

    public async Task<Designer> GetDetailsAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var designer = await _dbContext.Set<Designer>()
                .Include(x => x.Licenses)
                .ThenInclude(x => x.Branches)
                .ThenInclude(x => x.Branch)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            return designer;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
