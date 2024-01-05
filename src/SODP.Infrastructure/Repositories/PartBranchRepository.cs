using Microsoft.Extensions.Logging;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;

namespace SODP.Infrastructure.Repositories;

public sealed class PartBranchRepository : Repository<PartBranch>, IPartBranchRepository
{
    public PartBranchRepository(SODPDBContext dbContext, ILogger<PartBranch> logger)
        : base(dbContext, logger) { }
}
