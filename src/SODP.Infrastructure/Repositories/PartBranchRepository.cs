using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;

namespace SODP.Infrastructure.Repositories;

public sealed class PartBranchRepository : Repository<PartBranch>, IPartBranchRepository
{
    public PartBranchRepository(SODPDBContext dbContext, ILogger<PartBranch> logger)
        : base(dbContext, logger) { }

    public void Delete(int id)
    {
        var entity = _dbContext.PartBranches.FirstOrDefault(b => b.Id == id);
        _dbContext.Entry(entity).State = EntityState.Deleted;
    }
}
